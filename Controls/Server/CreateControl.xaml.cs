﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CampaignReactor.Controls.Server {
    /// <summary>
    /// Interaction logic for CreateControl.xaml
    /// </summary>
    public partial class CreateControl : UserControl {
        public libCampaignReactor.Models.Server server { get; set; } = new libCampaignReactor.Models.Server();
        public CreateControl() {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.showDialogue("Create Complete", $"Server has been successfully created!");
            mainWindow.searchFlyout.Content = new CampaignReactor.Controls.Server.EditControl(mainWindow.client.getServerById(mainWindow.client.createServer(this.server)));
            mainWindow.searchFlyout.IsOpen = true;
            Pages.Server.ServerPage page = (Pages.Server.ServerPage)mainWindow.campaignView.Content;
            page.getSearchResults();
        }
    }
}