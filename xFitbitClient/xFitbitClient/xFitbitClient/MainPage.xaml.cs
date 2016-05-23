using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace xFitbitClient
{
    internal enum AuthState
    {
        authRequest,
        login,
        redirect
    }

    public partial class MainPage : ContentPage
    {
        private WebView webView;
        private AuthState aState;

        public MainPage()
        {
            InitializeComponent();
           // InitializeComponent();
            //webView = this.FindByName<WebView>("webView1");
        }

        public void BtnGetProfile_Clicked(object sender, EventArgs args)
        {
            //webView.Source = "https://www.fitbit.com/oauth2/authorize?response_type=token&client_id=227M43&redirectUri=http://seshuk.com/fitbitdataservice/&scope=activity%20location%20profile&expires_in=604800";
            //aState = AuthState.authRequest;
            FitbitProfileHelper helper = new FitbitProfileHelper();
            var profile = helper.GetUserProfile();
        }

        void webOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.ToString().StartsWith("https://www.fitbit.com/login") && aState != AuthState.login)
                aState = AuthState.login;
            if(e.Url.ToString().StartsWith("http://seshuk.com/fitbit"))
            {
                e.Cancel = true;
                // Auth completed. Get the token and start the requests...
                webView.IsVisible = false;
                
            }
            
            if(e.Url.ToString().StartsWith("https://www.fitbit.com/oauth2/") && 
                aState == AuthState.login && 
                !e.Url.ToString().Contains("http://seshuk.com/fitbitdataservice"))
                webView.Source = "https://www.fitbit.com/oauth2/authorize?response_type=token&client_id=227M43&redirectUri=http://seshuk.com/fitbitdataservice/&scope=activity%20location%20profile&expires_in=604800";

            System.Diagnostics.Debug.WriteLine(e.Url);
        }
    }
}
