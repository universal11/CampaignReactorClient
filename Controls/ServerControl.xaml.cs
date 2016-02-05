using libCampaignReactor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CampaignReactorClient.Controls {
    public partial class ServerControl : UserControl, INotifyPropertyChanged {
        public ObservableCollection<Server> servers { get; set; } = new ObservableCollection<Server>();
        public CampaignReactorClient client { get; set; } = null;

        public Server _selectedServer { get; set; } = null;

        public Server selectedServer {
            get {
                return this._selectedServer;
            }
            set {
                this._selectedServer = value; NotifyPropertyChanged("selectedServer");
            }
        }


        private Visibility _viewPivotItemVisibility { get; set; } = Visibility.Collapsed;

        public Visibility viewPivotItemVisibility {
            get {
                return this._viewPivotItemVisibility;
            }
            set {
                this._viewPivotItemVisibility = value; NotifyPropertyChanged("viewPivotItemVisibility");
            }
        }

        public Server _newServer { get; set; } = new Server();
        public Server newServer {
            get { return this._newServer; }
            set { this._newServer = value; NotifyPropertyChanged("newServer"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ServerControl() {
            this.InitializeComponent();
        }

        public void loadServers(List<Server> servers) {
            this.servers.Clear();
            foreach (Server server in servers) {
                this.servers.Add(server);
            }
        }


        private void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void searchServers() {
            if (!string.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                this.loadServers(this.client.searchServers(this.searchTextBox.Text));
            }
            else {
                this.loadEnabledServers();
            }
        }

        public void loadEnabledServers() {
            this.loadServers(this.client.getEnabledServers());
        }

        public void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Server server = ((Server)((ListView)sender).SelectedItem);
            if (server != null) {
                this.selectedServer = server;
                this.viewPivotItemVisibility = Visibility.Visible;
            }
            else {
                this.viewPivotItemVisibility = Visibility.Collapsed;
            }

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int tabIndex = ((PivotItem)((Pivot)sender).SelectedItem).TabIndex;

            if (tabIndex.Equals(browsePivotItem.TabIndex)) {
                this.searchServers();
                if (this.selectedServer != null) {
                    this.selectServerById(this.selectedServer.id);
                }
            }
            else if (tabIndex.Equals(viewPivotItem.TabIndex)) {

            }
            else if (tabIndex.Equals(addPivotItem.TabIndex)) {

            }
        }

        private void selectServerById(int id) {

            for (int i = 0; i < this.listView.Items.Count; i++) {
                Server server = (Server)this.listView.Items[i];
                if (server.id.Equals(id)) {
                    this.listView.SelectedIndex = i;
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            Server server = this.client.getServerById(this.client.createServer(this.newServer));
            this.selectedServer = server;
            this.newServer = new Server();
            MainPage.showDialogue("Server Created!");
            this.pivot.SelectedIndex = viewPivotItem.TabIndex;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            this.client.updateServer(this.selectedServer);
            MainPage.showDialogue("Server Updated!");
            this.pivot.SelectedIndex = browsePivotItem.TabIndex;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.searchServers();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.searchServers();
        }


    }
}
