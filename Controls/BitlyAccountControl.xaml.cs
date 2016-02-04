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
    public partial class BitlyAccountControl : UserControl, INotifyPropertyChanged {
        public ObservableCollection<BitlyAccount> bitlyAccounts { get; set; } = new ObservableCollection<BitlyAccount>();
        public CampaignReactorClient client { get; set; } = null;

        public BitlyAccount _selectedBitlyAccount { get; set; } = null;

        public BitlyAccount selectedBitlyAccount {
            get {
                return this._selectedBitlyAccount;
            }
            set {
                this._selectedBitlyAccount = value; NotifyPropertyChanged("selectedBitlyAccount");
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

        public BitlyAccount _newBitlyAccount { get; set; } = new BitlyAccount();
        public BitlyAccount newBitlyAccount {
            get { return this._newBitlyAccount; }
            set { this._newBitlyAccount = value; NotifyPropertyChanged("newBitlyAccount"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BitlyAccountControl() {
            this.InitializeComponent();
        }


        public void loadBitlyAccounts(List<BitlyAccount> bitlyAccounts) {
            this.bitlyAccounts.Clear();
            foreach (BitlyAccount bitlyAccount in bitlyAccounts) {
                this.bitlyAccounts.Add(bitlyAccount);
            }
        }

        private void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void searchBitlyAccounts() {
            if (!string.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                this.loadBitlyAccounts(this.client.searchBitlyAccounts(this.searchTextBox.Text));
            }
            else {
                this.loadBitlyAccounts(new List<BitlyAccount>());
            }
        }

        public void loadEnabledBitlyAccounts() {
            this.loadBitlyAccounts(this.client.getEnabledBitlyAccounts());
        }

        public void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BitlyAccount bitlyAccount = ((BitlyAccount)((ListView)sender).SelectedItem);
            if (bitlyAccount != null) {
                this.selectedBitlyAccount = bitlyAccount;
                this.viewPivotItemVisibility = Visibility.Visible;
            }
            else {
                this.viewPivotItemVisibility = Visibility.Collapsed;
            }

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int tabIndex = ((PivotItem)((Pivot)sender).SelectedItem).TabIndex;

            if (tabIndex.Equals(browsePivotItem.TabIndex)) {
                this.searchBitlyAccounts();
                if (this.selectedBitlyAccount != null) {
                    this.selectBitlyAccountById(this.selectedBitlyAccount.id);
                }
            }
            else if (tabIndex.Equals(viewPivotItem.TabIndex)) {

            }
            else if (tabIndex.Equals(addPivotItem.TabIndex)) {

            }
        }

        private void selectBitlyAccountById(int id) {

            for (int i = 0; i < this.listView.Items.Count; i++) {
                BitlyAccount bitlyAccount = (BitlyAccount)this.listView.Items[i];
                if (bitlyAccount.id.Equals(id)) {
                    this.listView.SelectedIndex = i;
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            BitlyAccount bitlyAccount = this.client.getBitlyAccountById(this.client.createBitlyAccount(this.newBitlyAccount));
            this.selectedBitlyAccount = bitlyAccount;
            this.newBitlyAccount = new BitlyAccount();
            MainPage.showDialogue("Bitly Account Created!");
            this.pivot.SelectedIndex = viewPivotItem.TabIndex;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            this.client.updateBitlyAccount(this.selectedBitlyAccount);
            MainPage.showDialogue("Bitly Account Updated!");
            this.pivot.SelectedIndex = browsePivotItem.TabIndex;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.searchBitlyAccounts();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.searchBitlyAccounts();
        }


    }
}
