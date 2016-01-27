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

namespace CampaignReactor.Pages.BitlyAccount {
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class BitlyAccountPage : Page {
        public ObservableCollection<libCampaignReactor.Models.BitlyAccount> bitlyAccounts { get; set; } = new ObservableCollection<libCampaignReactor.Models.BitlyAccount>();
        public BitlyAccountPage() {
            InitializeComponent();
        }

        public void getEnabledBitlyAccounts() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.bitlyAccounts.Clear();
            foreach (libCampaignReactor.Models.BitlyAccount bitlyAccount in mainWindow.client.getEnabledBitlyAccounts()) {
                this.bitlyAccounts.Add(bitlyAccount);
            }

        }


        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.getSearchResults();
        }

        public void getSearchResults() {
            this.bitlyAccounts.Clear();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.BitlyAccount> bitlyAccounts = new List<libCampaignReactor.Models.BitlyAccount>();
            if (!String.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                bitlyAccounts = mainWindow.client.getEnabledBitlyAccounts();
            }
            else {
                bitlyAccounts = mainWindow.client.getAllBitlyAccounts();
            }

            foreach (libCampaignReactor.Models.BitlyAccount bitlyAccount in bitlyAccounts) {
                this.bitlyAccounts.Add(bitlyAccount);
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
            libCampaignReactor.Models.BitlyAccount bitlyAccount = (libCampaignReactor.Models.BitlyAccount)dataGridRow.Item;
            mainWindow.searchFlyout.Content = new Controls.BitlyAccount.EditControl(mainWindow.client.getBitlyAccountById(bitlyAccount.id));
            mainWindow.searchFlyout.IsOpen = true;
        }



        private void disableMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.BitlyAccount bitlyAccount = (libCampaignReactor.Models.BitlyAccount)dataGrid.SelectedItem;
                bitlyAccount.enabled = false;
                mainWindow.client.updateBitlyAccount(bitlyAccount);
                this.bitlyAccounts.Remove(bitlyAccount);
            }
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.BitlyAccount bitlyAccount = (libCampaignReactor.Models.BitlyAccount)dataGrid.SelectedItem;
                mainWindow.searchFlyout.Content = new Controls.BitlyAccount.EditControl(mainWindow.client.getBitlyAccountById(bitlyAccount.id));
                mainWindow.searchFlyout.IsOpen = true;

            }   
        }

        private void createMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.searchFlyout.Content = new Controls.BitlyAccount.CreateControl();
            mainWindow.searchFlyout.IsOpen = true;
        }
    }
}
