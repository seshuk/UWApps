using System;
using Xamarin.Forms;

namespace xFitbitClient
{
    internal enum AuthState
    {
        authRequest,
        login,
        redirect
    }

    public partial class ProfileViewPage : ContentPage
    {
        private WebView webView;
        private AuthState aState;
        private StackLayout profileViewLayout;
        private ActivityIndicator busyIndicator;

        public ProfileViewPage()
        {
            InitializeComponent();

            webView = this.FindByName<WebView>("webView1");
            webView.IsVisible = false;
            profileViewLayout = this.FindByName<StackLayout>("userProfileview");

            busyIndicator = new ActivityIndicator();
        }

        public async void BtnGetProfile_Clicked(object sender, EventArgs args)
        {
            profileViewLayout.Children.Add(busyIndicator);
            busyIndicator.IsRunning = true;
            FitbitProfileHelper helper = new FitbitProfileHelper();
            var profile = await helper.GetUserProfile();
            profileViewLayout.BindingContext = profile.User;
            busyIndicator.IsRunning = false;
            profileViewLayout.Children.Remove(busyIndicator);
        }

        void webOnNavigating(object sender, WebNavigatingEventArgs e)
        {
        }
    }
}
