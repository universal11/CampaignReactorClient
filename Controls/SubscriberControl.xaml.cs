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
    public sealed partial class SubscriberControl : UserControl {
        public ObservableCollection<Subscriber> subscribers { get; set; } = new ObservableCollection<Subscriber>();

        public SubscriberControl() {
            this.InitializeComponent();
        }


        public void loadSubscribers(List<Subscriber> subscribers) {
            this.subscribers.Clear();
            foreach (Subscriber subscriber in subscribers) {
                this.subscribers.Add(subscriber);
            }
        }
    }
}
