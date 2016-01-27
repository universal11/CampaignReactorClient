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
    /// Interaction logic for CreateControl.xaml
    /// </summary>
    public partial class CreateControl : UserControl {
        public libCampaignReactor.Models.Campaign campaign { get; set; } = new libCampaignReactor.Models.Campaign();
        public CreateControl() {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.showDialogue("Create Complete", $"Campaign has been successfully created!");
            mainWindow.searchFlyout.Content = new CampaignReactor.Controls.Campaign.EditControl(mainWindow.client.getCampaignById(mainWindow.client.createCampaign(this.campaign)));
            mainWindow.searchFlyout.IsOpen = true;
            Pages.Campaign.CampaignPage page = (Pages.Campaign.CampaignPage)mainWindow.campaignView.Content;
            page.getSearchResults();
        }


    }
}
