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
using Windows.UI.Input;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using CampaignReactorClient.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CampaignReactorClient{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        //public ObservableCollection<dynamic> items { get; set; } = new ObservableCollection<dynamic>();
        public CampaignReactorClient client { get; set; } = new CampaignReactorClient();
        
        
        

        public MainPage() {
            this.InitializeComponent();
            this.init();
        }

        private void init() {
            //this.initCampaignControl();
        }

        private void initCampaignControl() {
            //this.campaignControl.AddButtonClick += addCampaign;
            //this.campaignControl.UpdateButtonClick += updateCampaign;
        }

        
        private void navPivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Pivot pivot = (Pivot)sender;
            this.showProgressRing();

            if (pivot.SelectedIndex.Equals(dashboardPivotItem.TabIndex)) {

            }
            else if (pivot.SelectedIndex.Equals(campaignPivotItem.TabIndex)) {
                //this.campaignControl.loadEnabledCampaigns();
                
            }
            else if (pivot.SelectedIndex.Equals(serverPivotItem.TabIndex)) {
                //this.serverControl.loadServers(this.client.getEnabledServers());

            }
            else if (pivot.SelectedIndex.Equals(subscriberPivotItem.TabIndex)) {
                //this.subscriberControl.loadSubscribers(this.client.getEnabledSubscribers());
            }
            else if (pivot.SelectedIndex.Equals(botPivotItem.TabIndex)) {

            }
            else if (pivot.SelectedIndex.Equals(bitlyAccountPivotItem.TabIndex)) {

            }
            this.hideProgressRing();
        }

        public static void showConfirmationDialogue(string message) {
            MessageDialog messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand("Yes"));
            messageDialog.Commands.Add(new UICommand("No"));
            messageDialog.ShowAsync();
        }

        public static void showDialogue(string message) {
            MessageDialog messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand("Ok"));
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



    }

}
