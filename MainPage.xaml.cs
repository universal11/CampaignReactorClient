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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CampaignReactorClient{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page{

        public MainPage() {
            this.InitializeComponent();
        }


        /*
        public CampaignReactorClient client = new CampaignReactorClient();
        public Timer refreshTimer = new Timer(10000);

        public MainPage(){
            this.InitializeComponent();
            //this.client.init("campaignreactor.com", 3579);
            Subscriber subscriber = client.getSubscriberById(1);
            this.refreshTimer.Elapsed += new ElapsedEventHandler(refreshPage);
        }

        public void showDashboardView() {
            System.Console.WriteLine("Showing Dashboard!");
            this.dashboardView.Children.Clear();
            //this.dashboardView.Children.Add(new CampaignControl(client.getCurrentCampaign()));
        }



        public static Image getImage(string path) {
            System.Console.WriteLine("Loading Image: " + path);
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
            return image;
        }


        private void loadPage() {
            this.searchFlyout.IsOpen = false;
            if (this.navTabControl.SelectedIndex.Equals(dashboardTabItem.TabIndex)) {
                this.showDashboardView();
            }
            else if (this.navTabControl.SelectedIndex.Equals(campaignTabItem.TabIndex)) {
                Pages.Campaign.CampaignPage page = new Pages.Campaign.CampaignPage();
                page.getEnabledCampaigns();
                this.campaignView.Navigate(page);
            }
            else if (this.navTabControl.SelectedIndex.Equals(serverTabItem.TabIndex)) {
                Pages.Server.ServerPage page = new Pages.Server.ServerPage();
                page.getEnabledServers();
                this.serverView.Navigate(page);
            }
            else if (this.navTabControl.SelectedIndex.Equals(subscriberTabItem.TabIndex)) {
                Pages.Subscriber.SubscriberPage page = new Pages.Subscriber.SubscriberPage();
                page.getEnabledSubscribers();
                this.subscriberView.Navigate(page);
            }
            else if (this.navTabControl.SelectedIndex.Equals(botTabItem.TabIndex)) {
                Pages.Bot.BotPage page = new Pages.Bot.BotPage();
                page.getEnabledBots();
                this.botView.Navigate(page);


            }
            else if (this.navTabControl.SelectedIndex.Equals(bitlyAccountTabItem.TabIndex)) {
                Pages.BitlyAccount.BitlyAccountPage page = new Pages.BitlyAccount.BitlyAccountPage();
                page.getEnabledBitlyAccounts();
                this.bitlyAccountView.Navigate(page);

       
            }

            //this.startRefreshMonitor();
        }

        private void navTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.Source == this.navTabControl) {
                this.loadPage();
            }
        }


        private void refreshPage(object sender, ElapsedEventArgs e) {
            this.Dispatcher.Invoke((Action)(() => {
                this.loadPage();
                //this.showDialogue("Update Complete", "Data Successfully Refreshed!");
            }));

        }

        private void startRefreshMonitor() {
            this.refreshTimer.Enabled = true;
        }

        private void stopRefreshMonitor() {
            this.refreshTimer.Enabled = false;
        }

        public async void showDialogue(string title, string message) {
            await this.ShowMessageAsync(title, message);

        }

        private void campaignNavTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void helpButton_Click(object sender, RoutedEventArgs e) {
            this.showDialogue("Help", "Help section pending...");
        }

        private void accountButton_Click(object sender, RoutedEventArgs e) {
            this.showDialogue("Account", "Account section pending...");
        }

        private void dashboardTabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {

        }

        private void campaignTabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            campaignTabItem.IsSelected = true;
        }

        private void serverTabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            serverTabItem.IsSelected = true;
        }

        private void botTabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            botTabItem.IsSelected = true;
        }

        private void dashboardTabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {

        }

        private void campaignTabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            //this.campaignContextMenu.IsOpen = true;
        }



        private void subscriberTabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            subscriberTabItem.IsSelected = true;
        }

        private void bitlyAccountTabItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            bitlyAccountTabItem.IsSelected = true;
        }
        */
    }


}
