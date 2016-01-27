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

namespace CampaignReactor.Controls.BitlyAccount {
    /// <summary>
    /// Interaction logic for CreateControl.xaml
    /// </summary>
    public partial class CreateControl : UserControl {
        public libCampaignReactor.Models.BitlyAccount bitlyAccount { get; set; } = new libCampaignReactor.Models.BitlyAccount();

        public CreateControl() {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.showDialogue("Create Complete", $"Bitly Account has been successfully created!");
            mainWindow.searchFlyout.Content = new CampaignReactor.Controls.BitlyAccount.EditControl(mainWindow.client.getBitlyAccountById(mainWindow.client.createBitlyAccount(this.bitlyAccount)));
            mainWindow.searchFlyout.IsOpen = true;
            Pages.BitlyAccount.BitlyAccountPage page = (Pages.BitlyAccount.BitlyAccountPage)mainWindow.bitlyAccountView.Content;
            page.getSearchResults();
        }
    }
}
