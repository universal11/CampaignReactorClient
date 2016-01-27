using libCampaignReactor.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CampaignReactor.Controls.Subscriber {
    /// <summary>
    /// Interaction logic for EditControl.xaml
    /// </summary>
    public partial class EditControl : UserControl {
        public libCampaignReactor.Models.Subscriber subscriber { get; set; } = new libCampaignReactor.Models.Subscriber();
        public ObservableCollection<libCampaignReactor.Models.Bot> availableBots { get; set; } = new ObservableCollection<libCampaignReactor.Models.Bot>();
        public libCampaignReactor.Models.Bot bot { get; set; } = new libCampaignReactor.Models.Bot();
        public ObservableCollection<EventLog> eventLogs { get; set; } = new ObservableCollection<EventLog>();


        public EditControl(libCampaignReactor.Models.Subscriber subscriber) {
            this.init(subscriber);
            InitializeComponent();
            this.selectBot();
        }

        public void init(libCampaignReactor.Models.Subscriber subscriber) {
            this.subscriber = subscriber;
            this.getAvailableBots();
            this.getEventLogs();
        }

        public void selectBot() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.bot = mainWindow.client.getBotById(this.subscriber.botId);
            if (this.bot != null) {
                for (int i = 0; i < this.botSplitButton.Items.Count; i++) {
                    libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)this.botSplitButton.Items[i];
                    if (this.bot.id == bot.id) {
                        this.botSplitButton.SelectedIndex = i;
                        return;
                    }
                }
            }

        }

        public bool formIsValid() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            bool isValid = true;

            if (this.botSplitButton.SelectedItem == null) {
                mainWindow.showDialogue("Invalid Request", "Please select a bot.");
                isValid = false;
            }

            return isValid;
        }

        public void getAvailableBots() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.availableBots = new ObservableCollection<libCampaignReactor.Models.Bot>(mainWindow.client.getEnabledBots());
            libCampaignReactor.Models.Bot bot = new libCampaignReactor.Models.Bot();
            bot.emailAddress = "None";
            this.availableBots.Insert(0, bot);
        }



        private void updateButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.client.updateSubscriber(this.subscriber);
            mainWindow.showDialogue("Update Complete", $"Subscriber has been successfully updated!");
            mainWindow.searchFlyout.IsOpen = false;
            Pages.Subscriber.SubscriberPage page = (Pages.Subscriber.SubscriberPage)mainWindow.subscriberView.Content;
            page.getSearchResults();
        }

        public void getEventLogs() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.eventLogs.Clear();
            foreach (EventLog eventLog in mainWindow.client.getEventLogsBySubscriberId(this.subscriber.id)) {
                this.eventLogs.Add(eventLog);
            }
        }

        private void getEventLogsButton_Click(object sender, RoutedEventArgs e) {
            this.getEventLogs();
        }

        private void botSplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            SplitButton splitButton = (SplitButton)sender;
            libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)splitButton.SelectedItem;
            this.subscriber.botId = bot.id;
        }

        private void unsubscribeButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.client.unsubscribe(this.subscriber);
            mainWindow.showDialogue("Unsubscribe Complete", $"Subscriber has been successfully unsubscribed!");
            mainWindow.searchFlyout.IsOpen = false;
            Pages.Subscriber.SubscriberPage page = (Pages.Subscriber.SubscriberPage)mainWindow.subscriberView.Content;
            page.getSearchResults();
        }
    }
}
