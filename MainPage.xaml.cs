using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using libCampaignReactor.Models;
using CampaignReactorClient.Classes;
using Windows.UI.Input;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CampaignReactorClient{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        //public ObservableCollection<dynamic> items { get; set; } = new ObservableCollection<dynamic>();
        public CampaignReactor client = new CampaignReactor();
        public ObservableCollection<Campaign> campaigns { get; set; } = new ObservableCollection<Campaign>();
        public ObservableCollection<Server> servers { get; set; } = new ObservableCollection<Server>();
        public ObservableCollection<Subscriber> subscribers { get; set; } = new ObservableCollection<Subscriber>();

        public MainPage() {
            this.InitializeComponent();
        }

        private void navPivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Pivot pivot = (Pivot)sender;
            this.showProgressRing();

            if (pivot.SelectedIndex.Equals(dashboardPivotItem.TabIndex)) {

            }
            else if (pivot.SelectedIndex.Equals(campaignPivotItem.TabIndex)) {
                this.campaigns.Clear();
                foreach (Campaign campaign in this.client.getEnabledCampaigns()) {
                    this.campaigns.Add(campaign);
                }
            }
            else if (pivot.SelectedIndex.Equals(serverPivotItem.TabIndex)) {
                this.servers.Clear();
                foreach (Server server in this.client.getEnabledServers()) {
                    this.servers.Add(server);
                }

            }
            else if (pivot.SelectedIndex.Equals(subscriberPivotItem.TabIndex)) {
                this.subscribers.Clear();
                foreach (Subscriber subscriber in this.client.getEnabledSubscribers()) {
                    this.subscribers.Add(subscriber);
                }
            }
            else if (pivot.SelectedIndex.Equals(botPivotItem.TabIndex)) {

            }
            else if (pivot.SelectedIndex.Equals(bitlyAccountPivotItem.TabIndex)) {

            }
            this.hideProgressRing();
        }

        private void showDialogue(string message) {
            var messageDialog = new MessageDialog(message);

            messageDialog.Commands.Add(new UICommand("Yes"));

            messageDialog.Commands.Add(new UICommand("No"));



            // call the ShowAsync() method to display the message dialog

            messageDialog.ShowAsync();
        }

        public void showProgressRing() {
            this.splitView.IsPaneOpen = true;
        }

        public void hideProgressRing() {
            this.splitView.IsPaneOpen = false;
        }

        public void showPane() {
            this.splitView.IsPaneOpen = true;
        }

        public void hidePane() {
            this.splitView.IsPaneOpen = false;
        }

        private void togglePaneButton_Click(object sender, RoutedEventArgs e) {
            this.splitView.IsPaneOpen = true;
        }

        private void campaignListView_Tapped(object sender, TappedRoutedEventArgs e) {
            this.showPane();
        }

        private void serverListView_Tapped(object sender, TappedRoutedEventArgs e) {
            this.showPane();
        }

        private void subscriberListView_Tapped(object sender, TappedRoutedEventArgs e) {
            this.showPane();
        }

        private void subscriberListItemButton_Click(object sender, RoutedEventArgs e) {

            this.showDialogue($"Selected Email Address: {((Subscriber)this.subscriberListView.SelectedItem).emailAddress}");
        }
    }

}
