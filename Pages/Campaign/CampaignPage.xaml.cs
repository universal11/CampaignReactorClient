using CampaignReactor.Controls;
using CampaignReactor.Pages;
using libCampaignReactor.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CampaignReactor.Pages.Campaign {
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class CampaignPage : Page {
        public ObservableCollection<libCampaignReactor.Models.Campaign> campaigns { get; set; } = new ObservableCollection<libCampaignReactor.Models.Campaign>();
        public CampaignPage() {
            InitializeComponent();
        }

        public void getEnabledCampaigns() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.campaigns.Clear();
            foreach (libCampaignReactor.Models.Campaign campaign in mainWindow.client.getEnabledCampaigns()) {
                this.campaigns.Add(campaign);
            }

        }


        public void getCampaignsByName(string name) {
            this.campaigns.Clear();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            foreach (libCampaignReactor.Models.Campaign campaign in mainWindow.client.getCampaignsByName(name)) {
                this.campaigns.Add(campaign);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.getSearchResults();
        }

        public void getSearchResults() {
            this.campaigns.Clear();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Campaign> campaigns = new List<libCampaignReactor.Models.Campaign>();
            if (!String.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                campaigns = mainWindow.client.getCampaignsByName(this.searchTextBox.Text.Trim());
            }
            else {
                campaigns = mainWindow.client.getEnabledCampaigns();
            }

            foreach (libCampaignReactor.Models.Campaign campaign in campaigns) {
                this.campaigns.Add(campaign);
            }
            
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e) {

            if (e.Key == Key.Enter) {
                this.getSearchResults();
                return;
            }

            if (this.searchTextBox.Text.Length >= 3) {
                this.getSearchResults();
            }
                
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) {

        }

        private void menuButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.dashboardFlyout.IsOpen = true;
        }


        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.searchFlyout.IsOpen = false;
        }

        private void listViewRow_DoubleClick(object sender, MouseButtonEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            DataGridRow dataGridRow = (DataGridRow)sender;
            libCampaignReactor.Models.Campaign campaign = (libCampaignReactor.Models.Campaign)dataGridRow.Item;
            mainWindow.searchFlyout.Content = new Controls.Campaign.EditControl(mainWindow.client.getCampaignById(campaign.id));
            mainWindow.searchFlyout.IsOpen = true;
        }



        private void disableMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Campaign campaign = (libCampaignReactor.Models.Campaign)dataGrid.SelectedItem;
                campaign.enabled = false;
                mainWindow.client.updateCampaign(campaign);
                this.campaigns.Remove(campaign);
            }
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Campaign campaign = (libCampaignReactor.Models.Campaign)dataGrid.SelectedItem;
                mainWindow.searchFlyout.Content = new Controls.Campaign.EditControl(mainWindow.client.getCampaignById(campaign.id));
                mainWindow.searchFlyout.IsOpen = true;

            }   
        }

        private void sendMenuItem_Click(object sender, RoutedEventArgs e) {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Campaign campaign = (libCampaignReactor.Models.Campaign)dataGrid.SelectedItem;
                this.confirmSend(campaign);
            }
        }
        private async void confirmSend(libCampaignReactor.Models.Campaign campaign) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MessageDialogResult result = await mainWindow.ShowMessageAsync("Confirm", "Would you like to proceed?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative) {
                this.send(mainWindow.client.getCampaignById(campaign.id));
            }
        }

        public async void send(libCampaignReactor.Models.Campaign campaign) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Bot> bots = mainWindow.client.getAvailableBots();
            if (bots.Count > 0) {
                ProgressDialogController progresDialogController = await mainWindow.ShowProgressAsync("Sending...", "Progress message");
                int numberOfBotsSent = 0;
                progresDialogController.SetProgress(numberOfBotsSent / bots.Count);
                foreach (libCampaignReactor.Models.Bot bot in bots) {
                    this.sendCampaign(campaign, bot);
                    mainWindow.client.markBotAsSent(bot);
                    numberOfBotsSent++;
                    progresDialogController.SetProgress(numberOfBotsSent/bots.Count);
                }
                await progresDialogController.CloseAsync();
                //mainWindow.showDialogue("Send Complete", "All messages have been queued for delivery!");
            }
            else {
                mainWindow.showDialogue("Unable to Send", "There are currently no available bots!");
            }

        }

        public async Task sendCampaign(libCampaignReactor.Models.Campaign campaign, libCampaignReactor.Models.Bot bot) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Subscriber> subscribers = mainWindow.client.getSendQueueByBotId(bot.id);
            libCampaignReactor.Models.Host host = mainWindow.client.getNextHost();

            if (subscribers.Count > 0) {
                foreach (libCampaignReactor.Models.Subscriber subscriber in subscribers) {
                    libCampaignReactor.Models.BitlyAccount bitlyAccount = mainWindow.client.getNextBitlyAccount();
                    MailMessage message = new MailMessage();

                    message.From = new MailAddress(bot.emailAddress, $"{bot.firstName} {bot.lastName}");
                    message.To.Add(subscriber.emailAddress);
                    message.Subject = campaign.subject;
                    message.Body = campaign.subject;

                    int sendTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    string campaignUrl = mainWindow.client.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/event_log/click/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");
                    string campaignImageUrl = mainWindow.client.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/event_log/open/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");
                    string campaignUnsubscribeUrl = mainWindow.client.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/event_log/unsubscribe/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");
                    string campaignUnsubscribeImageUrl = mainWindow.client.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/campaign/unsubscribe_image/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");

                    AlternateView html = AlternateView.CreateAlternateViewFromString($"<center><a href=\"{campaignUrl}\">{campaign.subject}</a><br/><br/><a href=\"{campaignUrl}\"><img style=\"width=\"300px\" height=\"200px\" src=\"{campaignImageUrl}\" /></a><br/><br/><a href=\"{campaignUnsubscribeUrl}\"><img style=\"width=\"300px\" height=\"200px\" src=\"{campaignUnsubscribeImageUrl}\" /></a></center>");
                    html.ContentType = new System.Net.Mime.ContentType("text/html");
                    message.AlternateViews.Add(html);

                    SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 587);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(bot.emailAddress, bot.password);
                    smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                    try {
                        smtpClient.SendAsync(message, message);
                    }
                    catch (Exception exception) {
                        mainWindow.showDialogue("Exception", exception.Message);

                    }
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
                //System.Console.WriteLine(e.Error.Message);
            }
            else {
                // prompt user with message sent!
                // as we have the message object we can also display who the message
                // was sent to etc 
                //mainWindow.showDialogue("Send Success", "Message Send Successfully!");
            }

            // finally dispose of the message
            if (mailMessage != null) {
                mailMessage.Dispose();
            }

        }

        private void createMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.searchFlyout.Content = new Controls.Campaign.CreateControl();
            mainWindow.searchFlyout.IsOpen = true;
        }
    }
}
