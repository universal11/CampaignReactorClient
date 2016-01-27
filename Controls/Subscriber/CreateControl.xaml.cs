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
    /// Interaction logic for CreateControl.xaml
    /// </summary>
    public partial class CreateControl : UserControl {
        public libCampaignReactor.Models.Subscriber subscriber { get; set; } = new libCampaignReactor.Models.Subscriber();
        public ObservableCollection<libCampaignReactor.Models.Bot> availableBots { get; set; } = new ObservableCollection<libCampaignReactor.Models.Bot>();
        public libCampaignReactor.Models.Bot bot { get; set; } = new libCampaignReactor.Models.Bot();

        public CreateControl() {
            this.init();

            InitializeComponent();
        }

        public void init() {
            this.getAvailableBots();
        }

        public void getAvailableBots() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            ObservableCollection<libCampaignReactor.Models.Bot> bots = new ObservableCollection<libCampaignReactor.Models.Bot>(mainWindow.client.getEnabledBots());
            libCampaignReactor.Models.Bot bot = new libCampaignReactor.Models.Bot();
            bot.emailAddress = "Select";
            bots.Insert(0, bot);
            this.availableBots = bots;
        }

        private void createButton_Click(object sender, RoutedEventArgs e) {
            if (this.formIsValid()) {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.showDialogue("Create Complete", $"Subscriber has been successfully created!");
                mainWindow.searchFlyout.Content = new CampaignReactor.Controls.Subscriber.EditControl(mainWindow.client.getSubscriberById(mainWindow.client.createSubscriber(this.subscriber)));
                mainWindow.searchFlyout.IsOpen = true;
                Pages.Subscriber.SubscriberPage page = (Pages.Subscriber.SubscriberPage)mainWindow.subscriberView.Content;
                page.getSearchResults();
            }

        }

        private void botSplitButton_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            SplitButton splitButton = (SplitButton)sender;
            libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)splitButton.SelectedItem;
            this.subscriber.botId = bot.id;
        }

        public bool formIsValid() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            bool isValid = true;

            if (this.subscriber.botId == 0) {
                mainWindow.showDialogue("Invalid Request", $"Please select a bot. Bot | ID: {this.bot.id}");
                isValid = false;
            }

            return isValid;
        }
    }
}
