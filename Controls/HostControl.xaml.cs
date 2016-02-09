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
    public partial class HostControl : UserControl, INotifyPropertyChanged {
        public ObservableCollection<Host> hosts { get; set; } = new ObservableCollection<Host>();
        public CampaignReactorClient client { get; set; } = null;

        public Host _selectedHost { get; set; } = null;

        public Host selectedHost {
            get {
                return this._selectedHost;
            }
            set {
                this._selectedHost = value; NotifyPropertyChanged("selectedHost");
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

        public Host _newHost { get; set; } = new Host();
        public Host newHost {
            get { return this._newHost; }
            set { this._newHost = value; NotifyPropertyChanged("newHost"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public HostControl() {
            this.InitializeComponent();
        }

        public void loadHosts(List<Host> hosts) {
            this.hosts.Clear();
            foreach (Host host in hosts) {
                this.hosts.Add(host);
            }
        }


        private void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void searchHosts() {
            if (!string.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                this.loadHosts(this.client.searchHosts(this.searchTextBox.Text));
            }
            else {
                this.loadEnabledHosts();
            }
        }

        public void loadHostsByServerId(int id) {
            this.loadHosts(this.client.getHostsByServerId(id));
        }

        public void loadEnabledHosts() {
            this.loadHosts(this.client.getEnabledHosts());
        }

        public void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Host host = ((Host)((ListView)sender).SelectedItem);
            if (host != null) {
                this.selectedHost = host;
                this.viewPivotItemVisibility = Visibility.Visible;
            }
            else {
                this.viewPivotItemVisibility = Visibility.Collapsed;
            }

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int tabIndex = ((PivotItem)((Pivot)sender).SelectedItem).TabIndex;

            if (tabIndex.Equals(browsePivotItem.TabIndex)) {
                this.searchHosts();
                if (this.selectedHost != null) {
                    this.selectHostById(this.selectedHost.id);
                }
            }
            else if (tabIndex.Equals(viewPivotItem.TabIndex)) {

            }
            else if (tabIndex.Equals(addPivotItem.TabIndex)) {

            }
        }

        private void selectHostById(int id) {

            for (int i = 0; i < this.listView.Items.Count; i++) {
                Host host = (Host)this.listView.Items[i];
                if (host.id.Equals(id)) {
                    this.listView.SelectedIndex = i;
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            Host host = this.client.getHostById(this.client.createHost(this.newHost));
            this.selectedHost = host;
            this.newHost = new Host();
            MainPage.showDialogue("Host Created!");
            this.pivot.SelectedIndex = viewPivotItem.TabIndex;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            this.client.updateHost(this.selectedHost);
            MainPage.showDialogue("Host Updated!");
            this.pivot.SelectedIndex = browsePivotItem.TabIndex;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.searchHosts();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.searchHosts();
        }


    }
}
