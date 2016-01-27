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

namespace CampaignReactor.Pages.Bot {
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class BotPage : Page {
        public ObservableCollection<libCampaignReactor.Models.Bot> bots { get; set; } = new ObservableCollection<libCampaignReactor.Models.Bot>();
        public BotPage() {
            InitializeComponent();
        }

        public void getEnabledBots() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.bots.Clear();
            foreach (libCampaignReactor.Models.Bot bot in mainWindow.client.getEnabledBots()) {
                this.bots.Add(bot);
            }

        }


        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.getSearchResults();
        }

        public void getSearchResults() {
            this.bots.Clear();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Bot> bots = new List<libCampaignReactor.Models.Bot>();
            if (!String.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                bots = mainWindow.client.getEnabledBots();
            }
            else {
                bots = mainWindow.client.getAllBots();
            }

            foreach (libCampaignReactor.Models.Bot bot in bots) {
                this.bots.Add(bot);
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
            libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)dataGridRow.Item;
            mainWindow.searchFlyout.Content = new Controls.Bot.EditControl(mainWindow.client.getBotById(bot.id));
            mainWindow.searchFlyout.IsOpen = true;
        }



        private void disableMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)dataGrid.SelectedItem;
                bot.enabled = false;
                mainWindow.client.updateBot(bot);
                this.bots.Remove(bot);
            }
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)dataGrid.SelectedItem;
                mainWindow.searchFlyout.Content = new Controls.Bot.EditControl(mainWindow.client.getBotById(bot.id));
                mainWindow.searchFlyout.IsOpen = true;

            }   
        }

        private void createMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.searchFlyout.Content = new Controls.Bot.CreateControl();
            mainWindow.searchFlyout.IsOpen = true;
        }

        private void harvestMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Bot bot = (libCampaignReactor.Models.Bot)dataGrid.SelectedItem;
                mainWindow.client.harvest(bot);


            }
        }
    }
}
