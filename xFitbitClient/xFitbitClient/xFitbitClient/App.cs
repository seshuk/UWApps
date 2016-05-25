using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace xFitbitClient
{
    public class App : Application
    {
        public App()
        {
            var appTabs = new TabbedPage
            {
                Title = "Fitbit Demo client",
                Children =
                {
                    new ProfileViewPage(),
                    new ActivityView()
                }
            };
            // The root page of your application
            //ProfileViewPage pge = new ProfileViewPage();
            
            MainPage = new NavigationPage(appTabs);
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
