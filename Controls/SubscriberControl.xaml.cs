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
    public partial class SubscriberControl : UserControl, INotifyPropertyChanged {
        public ObservableCollection<Subscriber> subscribers { get; set; } = new ObservableCollection<Subscriber>();
        public CampaignReactorClient client { get; set; } = null;

        public Subscriber _selectedSubscriber { get; set; } = null;

        public Subscriber selectedSubscriber {
            get {
                return this._selectedSubscriber;
            }
            set {
                this._selectedSubscriber = value; NotifyPropertyChanged("selectedSubscriber");
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

        public Subscriber _newSubscriber { get; set; } = new Subscriber();
        public Subscriber newSubscriber {
            get { return this._newSubscriber; }
            set { this._newSubscriber = value; NotifyPropertyChanged("newSubscriber"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SubscriberControl() {
            this.InitializeComponent();
        }


        public void loadSubscribers(List<Subscriber> subscribers) {
            this.subscribers.Clear();
            foreach (Subscriber subscriber in subscribers) {
                this.subscribers.Add(subscriber);
            }
        }

        private void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void searchSubscribers() {
            if (!string.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                this.loadSubscribers(this.client.searchSubscribers(this.searchTextBox.Text));
            }
            else {
                this.loadSubscribers(new List<Subscriber>());
            }
        }

        public void loadEnabledSubscribers() {
            this.loadSubscribers(this.client.getEnabledSubscribers());
        }

        public void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Subscriber subscriber = ((Subscriber)((ListView)sender).SelectedItem);
            if (subscriber != null) {
                this.selectedSubscriber = subscriber;
                this.viewPivotItemVisibility = Visibility.Visible;
            }
            else {
                this.viewPivotItemVisibility = Visibility.Collapsed;
            }

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int tabIndex = ((PivotItem)((Pivot)sender).SelectedItem).TabIndex;

            if (tabIndex.Equals(browsePivotItem.TabIndex)) {
                this.searchSubscribers();
                if (this.selectedSubscriber != null) {
                    this.selectSubscriberById(this.selectedSubscriber.id);
                }
            }
            else if (tabIndex.Equals(viewPivotItem.TabIndex)) {

            }
            else if (tabIndex.Equals(addPivotItem.TabIndex)) {

            }
        }

        private void selectSubscriberById(int id) {

            for (int i = 0; i < this.listView.Items.Count; i++) {
                Subscriber subscriber = (Subscriber)this.listView.Items[i];
                if (subscriber.id.Equals(id)) {
                    this.listView.SelectedIndex = i;
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            Subscriber subscriber = this.client.getSubscriberById(this.client.createSubscriber(this.newSubscriber));
            this.selectedSubscriber = subscriber;
            this.newSubscriber = new Subscriber();
            MainPage.showDialogue("Subscriber Created!");
            this.pivot.SelectedIndex = viewPivotItem.TabIndex;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            this.client.updateSubscriber(this.selectedSubscriber);
            MainPage.showDialogue("Subscriber Updated!");
            this.pivot.SelectedIndex = browsePivotItem.TabIndex;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.searchSubscribers();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.searchSubscribers();
        }


    }
}
