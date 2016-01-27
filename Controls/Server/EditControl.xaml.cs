using libCampaignReactor.Models;
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

namespace CampaignReactor.Controls.Server {
    /// <summary>
    /// Interaction logic for EditControl.xaml
    /// </summary>
    public partial class EditControl : UserControl {
        public libCampaignReactor.Models.Server server { get; set; } = new libCampaignReactor.Models.Server();
        public ObservableCollection<Host> hosts { get; set; } = new ObservableCollection<Host>();

        public object selectedItem { get; set; }



        public EditControl(libCampaignReactor.Models.Server server) {
            this.init(server);
            InitializeComponent();
        }

        public void init(libCampaignReactor.Models.Server server) {
            this.server = server;
            this.detectHosts();

        }


        private void updateButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.client.updateServer(this.server);
            mainWindow.showDialogue("Update Complete", $"Server has been successfully updated!");
            mainWindow.searchFlyout.IsOpen = false;
            Pages.Server.ServerPage page = (Pages.Server.ServerPage)mainWindow.serverView.Content;
            page.getSearchResults();
        }

        public void detectHosts() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.hosts.Clear();
            foreach (Host host in mainWindow.client.getHosts(this.server)) {
                this.hosts.Add(host);
            }
        }

        private void detectHostsButton_Click(object sender, RoutedEventArgs e) {
            this.detectHosts();
        }
    }
}
