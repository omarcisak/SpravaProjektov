using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPF_Wpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        ZoznamProjektov zoznamProjektov = new ZoznamProjektov();

        private void button1_Click(object sender, RoutedEventArgs e)
        {            
            //hack
            ZoznamProjektov page = new ZoznamProjektov();
            Base.Statics.SIGNED_USER = new ProjectService.User() { name="Fero" };
            NavigationService.Navigate(page);
            //-----

            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Zadajte email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Zadajte platny email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                
                //----HASH-----------------------------------------
                byte[] data = Encoding.ASCII.GetBytes(password);
                var sha1 = new SHA1CryptoServiceProvider();
                var sha1data = sha1.ComputeHash(data);
                var hashedPassword = BitConverter.ToString(sha1data);
                //--------------------------------------------------
                ServiceAgent sa = new ServiceAgent();
                List<ProjectService.User> users = sa.soapClient.GetUsers(email, hashedPassword).ToList();

                //ProjectService.SPF_WSSoapClient soapClient = new ProjectService.SPF_WSSoapClient();
                //soapClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(Properties.Settings.Default.CS_WS);
                //List<ProjectService.User> users = soapClient.GetUsers(email, hashedPassword).ToList();
               
                if (users.Count()>0)
                {
                    Base.Statics.SIGNED_USER = users.First();
                   //ZoznamProjektov page = new ZoznamProjektov();
                    NavigationService.Navigate(page);
                    
                }
                else
                {
                    errormessage.Text = "Sorry! Nepozname sa.";
                }
                
            }
        }

    }
}
