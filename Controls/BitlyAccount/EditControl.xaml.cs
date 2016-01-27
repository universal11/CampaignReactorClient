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
    /// Interaction logic for EditControl.xaml
    /// </summary>
    public partial class EditControl : UserControl {
        public libCampaignReactor.Models.BitlyAccount bitlyAccount { get; set; } = new libCampaignReactor.Models.BitlyAccount();
        public object selectedItem { get; set; }

        public EditControl(libCampaignReactor.Models.BitlyAccount bitlyAccount) {
            this.init(bitlyAccount);
            InitializeComponent();
        }

        public void init(libCampaignReactor.Models.BitlyAccount bitlyAccount) {
            this.bitlyAccount = bitlyAccount;
        }


        private void updateButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.client.updateBitlyAccount(this.bitlyAccount);
            mainWindow.showDialogue("Update Complete", $"Bitly Account has been successfully updated!");
            mainWindow.searchFlyout.IsOpen = false;
            Pages.BitlyAccount.BitlyAccountPage page = (Pages.BitlyAccount.BitlyAccountPage)mainWindow.bitlyAccountView.Content;
            page.getSearchResults();
        }
    }
}
