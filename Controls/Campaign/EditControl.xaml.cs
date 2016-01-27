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

namespace CampaignReactor.Controls.Campaign {
    /// <summary>
    /// Interaction logic for EditControl.xaml
    /// </summary>
    public partial class EditControl : UserControl {
        public libCampaignReactor.Models.Campaign campaign { get; set; } = new libCampaignReactor.Models.Campaign();
        public object selectedItem { get; set; }


        public EditControl(libCampaignReactor.Models.Campaign campaign) {
            this.campaign = campaign;
            InitializeComponent();
        }

  

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.client.updateCampaign(this.campaign);
            mainWindow.showDialogue("Update Complete", $"Campaign has been successfully updated!");
            mainWindow.searchFlyout.IsOpen = false;
            Pages.Campaign.CampaignPage page = (Pages.Campaign.CampaignPage)mainWindow.campaignView.Content;
            page.getSearchResults();
        }

        private void campaignReportTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

    }
}
