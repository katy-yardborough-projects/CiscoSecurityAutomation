using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace CiscoSecurityAutomation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(List<string> files)
        {
            int a = files.Count;
            System.Windows.Forms.MessageBox.Show("Error processing " + a.ToString() + " files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        public void populategrid()
        {

            Dictionary<string, string> vulns = new Dictionary<string, string>();
            vulns.Add("DTP Enabled", "The dynamic trunking protocol (DTP) was enabled on a number of interfaces for the assessed devices. This can allow a suitably-placed attacker to negotiate a trunk for use in a VLAN-hopping attack to compromise devices on adjacent networks.");
            vulns.Add("Cisco Type 5 Password Identified", "Passwords on the Cisco switch were weakly encrypted as “Type 5” MD5 hashes. These hashes rely on the relatively weak MD5 hash which is yet to be broken but believed to be flawed and offer insufficient protection against the capabilities of modern adversaries.");
            vulns.Add("Cisco Type 7 Password Identified", "Passwords configured on the Cisco network devices were weakly encoded as “type 7” ciphertext. This ciphertext could be trivially decoded by an attacker to reveal the plaintext password, which could subsequently be used to gain unauthorised access to the devices.");
            vulns.Add("Proxy ARP Enabled", "The default gateway had proxy ARP enabled, allowing network interfaces to act as a proxy for the Address Resolution Protocol (ARP). This can allow perimeter security to be compromised.");
            vulns.Add("SNMP Service Enabled", "The Simple Network Management Protocol (SNMP) service was enabled. The SNMP service exposes management data about the local machine.");
            

            

        }

    }
}
