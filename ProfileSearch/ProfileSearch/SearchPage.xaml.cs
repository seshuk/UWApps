using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProfileSearch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
            errorText.Text = "";
            errorBox.Visibility = Visibility.Collapsed;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchString = searchText.Text;
            Frame.Navigate(typeof(ProfileView), searchString);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string error = e.Parameter.ToString();

            // for now display error only if Profile page sent us the error.
            if (!string.IsNullOrEmpty(error) && !string.IsNullOrWhiteSpace(error) && e.NavigationMode != NavigationMode.Back)
            {
                errorText.Text = error;
                errorBox.Visibility = Visibility.Visible;
                
            }
        }
    }
}
