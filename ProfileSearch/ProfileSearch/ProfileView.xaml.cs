using ProfileHelper;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
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

                profileName.Text = "@" + userProfile.Profile_Name;
                profileUrl.Text = userProfile.Gravatar_Url;

                badgeCount.Text = userProfile.Badges.Length.ToString();// BadgeCout.ToString();
                jsPoints.Text = userProfile.Points.JavaScript.ToString(); // JavaScriptPoints.ToString();
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
