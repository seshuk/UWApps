using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ProfileHelper;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProfileSearch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfileView : Page
    {
        public ProfileView()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string searchString = e.Parameter.ToString();
            
            VisualStateManager.GoToState(this, "LoadingState", true);

            LoadData(searchString);
        }

        private async void LoadData(string searchString)
        {
            try
            {
                HideProfileView();

                var profileHelper = new TreehouseProfileHelper();
                var userProfile = await profileHelper.GetUserProfile(searchString);

                profileName.Text = userProfile.ProfileName;
                profileUrl.Text = userProfile.GravatarUrl;

                badgeCount.Text = userProfile.BadgeCout.ToString();
                jsPoints.Text = userProfile.JavaScriptPoints.ToString();
                var uri = new Uri(profileUrl.Text, UriKind.Absolute);
                profileImage.Source = new BitmapImage(uri);

                ShowProfileView();

                GoToNormalState(2);
            }
            catch (Exception ex)
            {
                var error = "Unable to find profile " + searchString + " (" + ex.Message + ")";
                Frame.Navigate(typeof(SearchPage), error);
            }
            
        }

        private void ShowProfileView()
        {
            imageGrid.Visibility = Visibility.Visible;
            badgesPanel.Visibility = Visibility.Visible;
            pointsPanel.Visibility = Visibility.Visible;
        }

        private void HideProfileView()
        {
            imageGrid.Visibility = Visibility.Collapsed;
            badgesPanel.Visibility = Visibility.Collapsed;
            pointsPanel.Visibility = Visibility.Collapsed;
        }

        private async void GoToNormalState(int delay)
        {
            //delay
            await Task.Delay(TimeSpan.FromSeconds(delay));
            //normal state
            VisualStateManager.GoToState(this, "NormalState", true);
        }
    }
}
