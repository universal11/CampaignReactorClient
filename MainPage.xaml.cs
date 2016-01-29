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

        public MainPage() {
            this.InitializeComponent();
        }

        private void navPivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Pivot pivot = (Pivot)sender;

            if (pivot.SelectedIndex.Equals(dashboardPivotItem.TabIndex)) {
                //this.listView.Items.Clear();
            }
            else if (pivot.SelectedIndex.Equals(campaignPivotItem.TabIndex)) {
                this.campaigns.Clear();
                foreach (Campaign campaign in this.client.getEnabledCampaigns()) {
                    this.campaigns.Add(campaign);
                }

            }
            else if (pivot.SelectedIndex.Equals(serverPivotItem.TabIndex)) {
                //this.listView.Items.Clear();
            }
            else if (pivot.SelectedIndex.Equals(subscriberPivotItem.TabIndex)) {
                //this.listView.Items.Clear();
            }
            else if (pivot.SelectedIndex.Equals(botPivotItem.TabIndex)) {
                //this.listView.Items.Clear();
            }
            else if (pivot.SelectedIndex.Equals(bitlyAccountPivotItem.TabIndex)) {
                //this.listView.Items.Clear();
            }
        }

        private void showDialogue(string message) {
            var messageDialog = new MessageDialog(message);

            messageDialog.Commands.Add(new UICommand("Yes"));

            messageDialog.Commands.Add(new UICommand("No"));



            // call the ShowAsync() method to display the message dialog

            messageDialog.ShowAsync();
        }

        private void togglePaneButton_Click(object sender, RoutedEventArgs e) {
            this.splitView.IsPaneOpen = true;
        }
    }


}
