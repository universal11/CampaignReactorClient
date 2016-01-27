using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace CampaignReactor.Controls.Send {
    /// <summary>
    /// Interaction logic for SendControl.xaml
    /// </summary>
    public partial class SendControl : UserControl {
        public libCampaignReactor.Models.Campaign campaign { get; set; } = new libCampaignReactor.Models.Campaign();
        //public List<libCampaignReactor.Models.Bot> bots { get; set; } = new List<libCampaignReactor.Models.Bot>();


        public SendControl(libCampaignReactor.Models.Campaign campaign) {
            this.init(campaign);
            InitializeComponent();
        }

        public void init(libCampaignReactor.Models.Campaign campaign) {
            this.campaign = campaign;
        }

        private void startButton_Click(object sender, RoutedEventArgs e) {
            this.send(campaign);
        }

        private void stopButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public void send(libCampaignReactor.Models.Campaign campaign) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            foreach (libCampaignReactor.Models.Bot bot in mainWindow.client.getEnabledBots()) {
                this.sendCampaign(campaign, bot);
            }
        }

        public async Task sendCampaign(libCampaignReactor.Models.Campaign campaign, libCampaignReactor.Models.Bot bot) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Subscriber> subscribers = mainWindow.client.getSendQueueByBotId(bot.id);

            string message = "This is just a test!";

            foreach (libCampaignReactor.Models.Subscriber subscriber in subscribers) {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(bot.emailAddress);
                mailMessage.To.Add(subscriber.emailAddress);
                mailMessage.Subject = campaign.subject;
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 587);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(bot.emailAddress, bot.password);
                smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                try {
                    smtpClient.SendAsync(mailMessage, mailMessage);
                }
                catch (Exception exception) {
                    mainWindow.showDialogue("Exception", exception.Message);

                }
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
