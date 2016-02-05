using libCampaignReactor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public partial class CampaignControl : UserControl, INotifyPropertyChanged {
        public ObservableCollection<Campaign> campaigns { get; set; } = new ObservableCollection<Campaign>();
        public CampaignReactorClient client { get; set; } = null;

        public Campaign _selectedCampaign { get; set; } = null;

        public Campaign selectedCampaign {
            get {
                return this._selectedCampaign;
            }
            set {
                this._selectedCampaign = value; NotifyPropertyChanged("selectedCampaign");
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

        public Campaign _newCampaign { get; set; } = new Campaign();
        public Campaign newCampaign {
            get { return _newCampaign; }
            set { _newCampaign = value;  NotifyPropertyChanged("newCampaign"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*
        public event RoutedEventHandler AddButtonClick {
            add { this.addButton.Click += value; }
            remove { this.addButton.Click -= value; }
        }

        public event RoutedEventHandler UpdateButtonClick {
            add { this.updateButton.Click += value; }
            remove { this.updateButton.Click -= value; }
        }
        */
        public CampaignControl() {
            this.InitializeComponent();
        }


        private void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void loadCampaigns(List<Campaign> campaigns) {
            this.campaigns.Clear();
            foreach (Campaign campaign in campaigns) {
                this.campaigns.Add(campaign);
            }
        }

        public void searchCampaigns() {
            if (!string.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                this.loadCampaigns(this.client.searchCampaigns(this.searchTextBox.Text));
            }
            else {
                this.loadEnabledCampaigns();
            }
        }

        public void loadEnabledCampaigns() {
            this.loadCampaigns(this.client.getEnabledCampaigns());
        }

        public void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Campaign campaign = ((Campaign)((ListView)sender).SelectedItem);
            if (campaign != null) {
                this.selectedCampaign = campaign;
                this.viewPivotItemVisibility = Visibility.Visible;
            }
            else {
                this.viewPivotItemVisibility = Visibility.Collapsed;
            }
            
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int tabIndex = ( (PivotItem)((Pivot)sender).SelectedItem ).TabIndex;

            if (tabIndex.Equals(browsePivotItem.TabIndex)) {
                this.searchCampaigns();
                if (this.selectedCampaign != null) {
                    this.selectCampaignById(this.selectedCampaign.id);
                }
            }
            else if (tabIndex.Equals(viewPivotItem.TabIndex)) {
       
            }
            else if (tabIndex.Equals(addPivotItem.TabIndex)) {

            }
        }

        private void selectCampaignById(int id) {

            for (int i = 0; i < this.listView.Items.Count; i++) {
                Campaign campaign = (Campaign)this.listView.Items[i];
                if (campaign.id.Equals(id)) {
                    this.listView.SelectedIndex = i;
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            Campaign campaign = this.client.getCampaignById(this.client.createCampaign(this.newCampaign));
            this.selectedCampaign = campaign;
            this.newCampaign = new Campaign();
            MainPage.showDialogue("Campaign Created!");
            this.pivot.SelectedIndex = viewPivotItem.TabIndex;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            this.client.updateCampaign(this.selectedCampaign);
            MainPage.showDialogue("Campaign Updated!");
            this.pivot.SelectedIndex = browsePivotItem.TabIndex;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
           this.searchCampaigns();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.searchCampaigns();
        }
    }

}
