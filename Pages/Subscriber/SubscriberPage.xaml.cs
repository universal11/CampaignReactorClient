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

namespace CampaignReactor.Pages.Subscriber {
    /// <summary>
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class SubscriberPage : Page {
        public ObservableCollection<libCampaignReactor.Models.Subscriber> subscribers { get; set; } = new ObservableCollection<libCampaignReactor.Models.Subscriber>();
        public SubscriberPage() {
            InitializeComponent();
        }

        public void getEnabledSubscribers() {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.subscribers.Clear();
            foreach (libCampaignReactor.Models.Subscriber subscriber in mainWindow.client.getEnabledSubscribers()) {
                this.subscribers.Add(subscriber);
            }

        }


        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.getSearchResults();
        }

        public void getSearchResults() {
            this.subscribers.Clear();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            List<libCampaignReactor.Models.Subscriber> subscribers = new List<libCampaignReactor.Models.Subscriber>();
            if (!String.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                subscribers = mainWindow.client.getEnabledSubscribers();
            }
            else {
                subscribers = mainWindow.client.getAllSubscribers();
            }

            foreach (libCampaignReactor.Models.Subscriber subscriber in subscribers) {
                this.subscribers.Add(subscriber);
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
            libCampaignReactor.Models.Subscriber subscriber = (libCampaignReactor.Models.Subscriber)dataGridRow.Item;
            mainWindow.searchFlyout.Content = new Controls.Subscriber.EditControl(mainWindow.client.getSubscriberById(subscriber.id));
            mainWindow.searchFlyout.IsOpen = true;
        }



        private void disableMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Subscriber subscriber = (libCampaignReactor.Models.Subscriber)dataGrid.SelectedItem;
                subscriber.enabled = false;
                mainWindow.client.updateSubscriber(subscriber);
                this.subscribers.Remove(subscriber);
            }
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu contextMenu = (ContextMenu)menuItem.Parent;
            DataGrid dataGrid = (DataGrid)contextMenu.PlacementTarget;
            if (dataGrid.SelectedItem != null) {
                libCampaignReactor.Models.Subscriber subscriber = (libCampaignReactor.Models.Subscriber)dataGrid.SelectedItem;
                mainWindow.searchFlyout.Content = new Controls.Subscriber.EditControl(mainWindow.client.getSubscriberById(subscriber.id));
                mainWindow.searchFlyout.IsOpen = true;

            }   
        }

        private void createMenuItem_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.searchFlyout.Content = new Controls.Subscriber.CreateControl();
            mainWindow.searchFlyout.IsOpen = true;
        }
    }
}
