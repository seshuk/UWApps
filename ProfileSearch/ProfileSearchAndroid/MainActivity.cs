using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace ProfileSearchAndroid
{
    [Activity(Label = "ProfileSearchAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
       // int count = 1;
        private EditText searchText;
        private Button searchButton;

        //private ProfileHelper.TreehouseProfileHelper helper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            searchText = FindViewById<EditText>(Resource.Id.editText1);
            searchButton = FindViewById<Button>(Resource.Id.SearchButton);

            searchButton.Click +=  delegate {
                //  await LoadData(searchText.Text);
                //button.Text = string.Format("{0} clicks!", count++);
                var intent = new Intent(this, typeof(ProfileViewActivity));
                intent.PutExtra("searchString", searchText.Text);
                StartActivity(intent);
            };
        }

      
    }
}

