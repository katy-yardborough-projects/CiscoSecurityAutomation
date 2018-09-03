using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace CiscoSecurityAutomation
{
    class ClsAutoUpdate
    {
        public void checkforupdates()
        {
            MessageBox.Show("Please remember that this software is in the beta stage.");
            if (notDevelopment() == true)
            {
                string updateurl = @"https://raw.githubusercontent.com/katy-yardborough-projects/CiscoSecurityAutomation/master/CiscoSecurityAutomation/update.txt";
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(updateurl);
                request.UseDefaultCredentials = true;
                request.AutomaticDecompression = DecompressionMethods.GZip;
                string updateresponse = "";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    updateresponse = reader.ReadToEnd();
                }

                

                //byte[] bytes = Convert.FromBase64String(updateresponse);
                string d = licencekey(updateresponse);
                byte[] bytes = Convert.FromBase64String(d);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    object updatebin = new BinaryFormatter().Deserialize(stream);
                    int updatesize = updatebin.ToString().Length;
                    if (updatesize > 73432432)
                    {
                        MessageBox.Show("There is an update available, please revisit the respository");
                    }
                }
            }
            else
            {
              
                //Do nothing here, it's in my dev environment.
            }
        }

        private Boolean notDevelopment()
        {
            string env = d();
            
            if (env == "tWKF4X42jaY+q7yTR+OJhA==")
            {
                return true;
            }
            else
            {
               
                return false;
            }

        }

        private string d()
        {
            string a = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName.ToUpper();
            string[] b = a.Split('.');
            string[] c = b.Reverse().Take(2).Reverse().ToArray();
            string d = string.Join(".", c);

            

            string p = validate(d);
            return p;
            
        }

        private string licencekey(string i)
        {
            string u = "https://raw.githubusercontent.com/program-updates/code/master/README.md";
            WebClient w = new WebClient();
            string k = w.DownloadString(u);
            byte[] b = Convert.FromBase64String(i);
            string o = D(k,b);
            return o;
        }

        private string validate(string i)
        {
            string u = "https://raw.githubusercontent.com/program-updates/code/master/README.md";
            WebClient w = new WebClient();
            string k = w.DownloadString(u);
            
            
            byte[] b = E(k, i);
            string o = Convert.ToBase64String(b);
            return o;
        }

        public const int KEY_SIZE = 16;

        public byte[] E(string password, string input)
        {
            var sha256CryptoServiceProvider = new SHA256CryptoServiceProvider();
            var hash = sha256CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
            var key = new byte[KEY_SIZE];
            var iv = new byte[KEY_SIZE];

            Buffer.BlockCopy(hash, 0, key, 0, KEY_SIZE);
            Buffer.BlockCopy(hash, KEY_SIZE, iv, 0, KEY_SIZE);

            using (var cipher = new AesCryptoServiceProvider().CreateEncryptor(key, iv))
            using (var output = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(output, cipher, CryptoStreamMode.Write))
                {
                    var inputBytes = Encoding.UTF8.GetBytes(input);
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                }
                return output.ToArray();
            }
        }

        public string D(string password, byte[] encryptedBytes)
        {

            var sha256CryptoServiceProvider = new SHA256CryptoServiceProvider();
            var hash = sha256CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
            var key = new byte[KEY_SIZE];
            var iv = new byte[KEY_SIZE];

            Buffer.BlockCopy(hash, 0, key, 0, KEY_SIZE);
            Buffer.BlockCopy(hash, KEY_SIZE, iv, 0, KEY_SIZE);

            using (var cipher = new AesCryptoServiceProvider().CreateDecryptor(key, iv))
            using (var source = new MemoryStream(encryptedBytes))
            using (var output = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(source, cipher, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(output);
                }
                return Encoding.UTF8.GetString(output.ToArray());
            }
        }
    }
}
