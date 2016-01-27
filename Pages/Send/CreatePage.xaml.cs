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
using libCampaignReactor.Models;
using System.Net.Mail;
using System.ComponentModel;
using System.Net;

namespace CampaignReactor.Pages.Send {
    /// <summary>
    /// Interaction logic for CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Page {
        public libCampaignReactor.Models.Campaign campaign { get; set; } = new libCampaignReactor.Models.Campaign();
        public List<libCampaignReactor.Models.Bot> bots { get; set; } = new List<libCampaignReactor.Models.Bot>();

        public CreatePage(libCampaignReactor.Models.Campaign campaign) {
            this.init(campaign);
            InitializeComponent();
        }

        public void init(libCampaignReactor.Models.Campaign campaign) {
            this.campaign = campaign;
        }

        private void startButton_Click(object sender, RoutedEventArgs e) {
            
            
            this.sendEmail("universal11@gmail.com", "test", "just a test");
           
            
        }

        private void stopButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        }

        public async Task sendEmail(string recipientAddress, string subject, string message) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("batdog623@yahoo.com");
            mailMessage.To.Add(recipientAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("batdog623@yahoo.com", "coolbeans11");
            smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            try {
                smtpClient.SendAsync(mailMessage, mailMessage);
            }
            catch (Exception exception) {
                mainWindow.showDialogue("Exception", exception.Message);
                
            }
        }

        private static void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e) {
            // Get the message we sent
            MailMessage mailMessage = (MailMessage)e.UserState;
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            if (e.Cancelled) {
                // prompt user with "send cancelled" message 
                mainWindow.showDialogue("Send Error", "Send Cancelled!");
            }
            if (e.Error != null) {
                // prompt user with error message 
                mainWindow.showDialogue("Send Error", e.Error.Message);
                System.Console.WriteLine(e.Error.Message);
            }
            else {
                // prompt user with message sent!
                // as we have the message object we can also display who the message
                // was sent to etc 
                mainWindow.showDialogue("Send Success", "Message Send Successfully!");
            }

            // finally dispose of the message
            if (mailMessage != null) {
                mailMessage.Dispose();
            }
                
        }

    }
}
