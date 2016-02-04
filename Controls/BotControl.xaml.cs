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
    public partial class BotControl : UserControl, INotifyPropertyChanged {
        public ObservableCollection<Bot> bots { get; set; } = new ObservableCollection<Bot>();
        public CampaignReactorClient client { get; set; } = null;

        public Bot _selectedBot { get; set; } = null;

        public Bot selectedBot {
            get {
                return this._selectedBot;
            }
            set {
                this._selectedBot = value; NotifyPropertyChanged("selectedBot");
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

        public Bot _newBot { get; set; } = new Bot();
        public Bot newBot {
            get { return this._newBot; }
            set { this._newBot = value; NotifyPropertyChanged("newBot"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BotControl() {
            this.InitializeComponent();
        }


        public void loadBots(List<Bot> bots) {
            this.bots.Clear();
            foreach (Bot bot in bots) {
                this.bots.Add(bot);
            }
        }

        private void NotifyPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void searchBots() {
            if (!string.IsNullOrEmpty(this.searchTextBox.Text.Trim())) {
                this.loadBots(this.client.searchBots(this.searchTextBox.Text));
            }
            else {
                this.loadBots(new List<Bot>());
            }
        }

        public void loadEnabledBots() {
            this.loadBots(this.client.getEnabledBots());
        }

        public void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Bot bot = ((Bot)((ListView)sender).SelectedItem);
            if (bot != null) {
                this.selectedBot = bot;
                this.viewPivotItemVisibility = Visibility.Visible;
            }
            else {
                this.viewPivotItemVisibility = Visibility.Collapsed;
            }

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int tabIndex = ((PivotItem)((Pivot)sender).SelectedItem).TabIndex;

            if (tabIndex.Equals(browsePivotItem.TabIndex)) {
                this.searchBots();
                if (this.selectedBot != null) {
                    this.selectBotById(this.selectedBot.id);
                }
            }
            else if (tabIndex.Equals(viewPivotItem.TabIndex)) {

            }
            else if (tabIndex.Equals(addPivotItem.TabIndex)) {

            }
        }

        private void selectBotById(int id) {

            for (int i = 0; i < this.listView.Items.Count; i++) {
                Bot bot = (Bot)this.listView.Items[i];
                if (bot.id.Equals(id)) {
                    this.listView.SelectedIndex = i;
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) {
            Bot bot = this.client.getBotById(this.client.createBot(this.newBot));
            this.selectedBot = bot;
            this.newBot = new Bot();
            MainPage.showDialogue("Bot Created!");
            this.pivot.SelectedIndex = viewPivotItem.TabIndex;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e) {
            this.client.updateBot(this.selectedBot);
            MainPage.showDialogue("Subscriber Updated!");
            this.pivot.SelectedIndex = browsePivotItem.TabIndex;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            this.searchBots();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            this.searchBots();
        }


    }
}
