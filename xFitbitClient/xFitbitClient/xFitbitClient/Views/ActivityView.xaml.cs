using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace xFitbitClient
{
    public partial class ActivityView : ContentPage
    {
        private ActivityIndicator busyIndicator;

        public ActivityView()
        {
            InitializeComponent();

            busyIndicator = new ActivityIndicator();
        }

        protected override async void OnAppearing()
        {
            busyIndicator.IsRunning = true;
            layoutRoot.Children.Add(busyIndicator);
            try
            {
                FitbitProfileHelper helper = new FitbitProfileHelper();
                var profile = await helper.GetUserProfile();
                badgesListView.ItemsSource = profile.User.TopBadges;
                busyIndicator.IsRunning = false;
                layoutRoot.Children.Remove(busyIndicator);
            }
            catch
            {

            }
        }

    }
}
