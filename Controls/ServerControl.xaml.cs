﻿using libCampaignReactor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ServerControl : UserControl {
        public ObservableCollection<Server> servers { get; set; } = new ObservableCollection<Server>();

        public ServerControl() {
            this.InitializeComponent();
        }

        public void loadServers(List<Server> servers) {
            this.servers.Clear();
            foreach (Server server in servers) {
                this.servers.Add(server);
            }
        }
    }
}
