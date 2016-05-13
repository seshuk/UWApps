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
            //mainContent.Visibility = Visibility.Collapsed;
            VisualStateManager.GoToState(this, "LoadingState", true);
            LoadData(searchString);
        }

        private async void LoadData(string searchString)
        {
            try
            {
               // errorBox.Visibility = Visibility.Collapsed;
                imageGrid.Visibility = Visibility.Collapsed;
                badgesPanel.Visibility = Visibility.Collapsed;
                pointsPanel.Visibility = Visibility.Collapsed;
                HttpWebRequest request = WebRequest.Create("https://teamtreehouse.com/" + searchString + ".json") as HttpWebRequest;
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    { // this is an error

                        return;
                    }

                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    dynamic profile = JsonConvert.DeserializeObject(reader.ReadToEnd());

                    profileName.Text = "@" + profile.name;
                    profileUrl.Text = profile.gravatar_url;

                    badgeCount.Text = profile.badges.Count.ToString();
                    jsPoints.Text = profile.points.JavaScript.ToString();
                    var uri = new Uri(profileUrl.Text, UriKind.Absolute);
                    profileImage.Source = new BitmapImage(uri);

                }
                //mainContent.Visibility = Visibility.Visible;
                imageGrid.Visibility = Visibility.Visible;
                badgesPanel.Visibility = Visibility.Visible;
                pointsPanel.Visibility = Visibility.Visible;
                GoToNormalState(2);
            }
            catch (Exception ex)
            {

                Debug.Write(ex);
                // errorText.Text = "Unable to find profile " + searchString + " (" + ex.Message + ")";
                // errorBox.Visibility = Visibility.Visible;
                var error = "Unable to find profile " + searchString + " (" + ex.Message + ")";
                Frame.Navigate(typeof(SearchPage), error);
                //imageGrid.Visibility = Visibility.Collapsed;
                //badgesPanel.Visibility = Visibility.Collapsed;
                //pointsPanel.Visibility = Visibility.Collapsed;
                //GoToNormalState(0);
            }
            
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
