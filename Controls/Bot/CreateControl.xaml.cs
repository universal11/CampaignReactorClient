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

namespace CampaignReactor.Controls.Bot {
    /// <summary>
    /// Interaction logic for CreateControl.xaml
    /// </summary>
    public partial class CreateControl : UserControl {
        public libCampaignReactor.Models.Bot bot { get; set; } = new libCampaignReactor.Models.Bot();
        public CreateControl() {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.showDialogue("Create Complete", $"Bot has been successfully created!");
            mainWindow.searchFlyout.Content = new CampaignReactor.Controls.Bot.EditControl(mainWindow.client.getBotById(mainWindow.client.createBot(this.bot)));
            mainWindow.searchFlyout.IsOpen = true;
            Pages.Bot.BotPage page = (Pages.Bot.BotPage)mainWindow.botView.Content;
            page.getSearchResults();
        }
    }
}
