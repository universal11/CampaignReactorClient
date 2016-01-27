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

namespace CampaignReactor.Controls.Bot {
    /// <summary>
    /// Interaction logic for EditControl.xaml
    /// </summary>
    public partial class EditControl : UserControl {
        public libCampaignReactor.Models.Bot bot { get; set; } = new libCampaignReactor.Models.Bot();
        public ObservableCollection<libCampaignReactor.Models.Subscriber> subscribers { get; set; } = new ObservableCollection<libCampaignReactor.Models.Subscriber>();

        public object selectedItem { get; set; }



        public EditControl(libCampaignReactor.Models.Bot bot) {
            this.init(bot);
            InitializeComponent();
        }

        public void init(libCampaignReactor.Models.Bot bot) {
            this.bot = bot;
            this.getSubscribers();
        }


        private void updateButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.client.updateBot(this.bot);
            mainWindow.showDialogue("Update Complete", $"Bot has been successfully updated!");
            mainWindow.searchFlyout.IsOpen = false;
            Pages.Bot.BotPage page = (Pages.Bot.BotPage)mainWindow.botView.Content;
            page.getSearchResults();
        }

        public void getSubscribers() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.subscribers.Clear();
            foreach (libCampaignReactor.Models.Subscriber subscribers in mainWindow.client.getSubscribersByBotId(this.bot.id)) {
                this.subscribers.Add(subscribers);
            }
        }

        private void getSubscribersButton_Click(object sender, RoutedEventArgs e) {
            this.getSubscribers();
        }
    }
}
