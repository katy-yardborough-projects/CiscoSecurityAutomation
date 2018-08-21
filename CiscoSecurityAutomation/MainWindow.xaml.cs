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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace CiscoSecurityAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClsAutoUpdate checkforupdate = new ClsAutoUpdate();
            checkforupdate.checkforupdates();
            lstFiles.Items.Add("Double click to choose files");
        }

        private void lstFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                lstFiles.Items.Clear();
                // Open document 
                foreach(string filename in dlg.FileNames)
                {
                    lstFiles.Items.Add(filename);
                }
                
            }
        }

        private void btnAnalyse_Click(object sender, RoutedEventArgs e)
        {
            Window1 results = new Window1(lstFiles.Items.Cast<string>().ToList());
            System.Windows.Forms.MessageBox.Show("Error processing " + lstFiles.Items.Count.ToString() + " files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            results.ShowDialog();
        }
    }
}
