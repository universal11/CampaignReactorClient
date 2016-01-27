using CampaignReactor.Controls;
using CampaignReactor.Pages;
using libCampaignReactor.Models;
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

namespace CampaignReactor.Pages.Server {
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class ServerPage : Page {
        public ObservableCollection<libCampaignReactor.Models.Server> servers { get; set; } = new ObservableCollection<libCampaignReactor.Models.Server>();
        public ServerPage() {
            InitializeComponent();
        }

        public void getEnabledServers() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.servers.Clear();
            foreach (libCampaignReactor.Models.Server server in mainWindow.client.getEnabledServers()) {
                this.servers.Add(server);
            }

        }


        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.getSearchResults();
        }

        public void getSearchResults() {
            this.servers.Clear();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Server> servers = new List<libCampaignReactor.Models.Server>();
            if (!String.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                servers = mainWindow.client.getEnabledServers();
            }
            else {
                servers = mainWindow.client.getAllServers();
            }

            foreach (libCampaignReactor.Models.Server server in servers) {
                this.servers.Add(server);
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
            libCampaignReactor.Models.Server server = (libCampaignReactor.Models.Server)dataGridRow.Item;
            mainWindow.searchFlyout.Content = new Controls.Server.EditControl(mainWindow.client.getServerById(server.id));
            mainWindow.searchFlyout.IsOpen = true;
        }



        private void disableMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Server server = (libCampaignReactor.Models.Server)dataGrid.SelectedItem;
                server.enabled = false;
                mainWindow.client.updateServer(server);
                this.servers.Remove(server);
            }
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Server server = (libCampaignReactor.Models.Server)dataGrid.SelectedItem;
                mainWindow.searchFlyout.Content = new Controls.Server.EditControl(mainWindow.client.getServerById(server.id));
                mainWindow.searchFlyout.IsOpen = true;

            }   
        }

        private void createMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.searchFlyout.Content = new Controls.Server.CreateControl();
            mainWindow.searchFlyout.IsOpen = true;
        }
    }
}
