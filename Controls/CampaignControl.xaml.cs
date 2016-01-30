using libCampaignReactor.Models;
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
    public sealed partial class CampaignControl : UserControl {
        public ObservableCollection<Campaign> campaigns { get; set; } = new ObservableCollection<Campaign>();
        public Campaign campaign {get; set;} = new Campaign();

        public event RoutedEventHandler AddButtonClick {
            add { this.addButton.Click += value; }
            remove { this.addButton.Click -= value; }
        }

        public CampaignControl() {
            this.InitializeComponent();
        }

        public void loadCampaigns(List<Campaign> campaigns) {
            this.campaigns.Clear();
            foreach (Campaign campaign in campaigns) {
                this.campaigns.Add(campaign);
            }
        }


    }

}
