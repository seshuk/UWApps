using System;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using ProfileHelper;

namespace ProfileSearchAndroid
{
    [Activity(Label = "Profile Details")]
    public class ProfileViewActivity : Activity
    {
        private TextView profileName;
        private TextView badgeCount;
        private TextView jsPoints;
        private ImageView profileImage;
        ProgressBar pb;
        TextView txtStatus;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
 
            SetContentView(Resource.Layout.ProfileView);

            profileName = FindViewById<TextView>(Resource.Id.textProfileName);
            badgeCount = FindViewById<TextView>(Resource.Id.textBadgeCount);
            jsPoints = FindViewById<TextView>(Resource.Id.textJavaScriptPoints);
            profileImage = FindViewById<ImageView>(Resource.Id.profileIcon);
            pb = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            txtStatus = FindViewById<TextView>(Resource.Id.textStatus);

            HideProfileControls();
            var searchString = Intent.Extras.GetString("searchString");
            pb.Indeterminate = true;
 
            LoadData(searchString);
            
        }

        private async void  LoadData(string searchString)
        {
            try
            {
                var profileHelper = new TreehouseProfileHelper();
                var userProfile = await profileHelper.GetUserProfile(searchString);

                profileName.Text = "@" + userProfile.Profile_Name;
                badgeCount.Text = userProfile.Badges.Length.ToString() + " Badges";
                jsPoints.Text = userProfile.Points.JavaScript.ToString() + " JavaScript Points";
                var bmp = GetImageBitmapFromUrl(userProfile.Gravatar_Url);
                profileImage.SetImageBitmap(GetRoundedShape(bmp));
                
                pb.Visibility = ViewStates.Gone;
                txtStatus.Visibility = ViewStates.Gone;
                ShowProfileControls();
            }
            catch (Exception ex)
            {
                pb.Visibility = ViewStates.Gone;
                txtStatus.Visibility = ViewStates.Gone;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                ShowAlert(ex.Message);
            }


        }

        private void ShowProfileControls()
        {
            profileImage.Visibility = ViewStates.Visible;
            profileName.Visibility = ViewStates.Visible;
            badgeCount.Visibility = ViewStates.Visible;
            jsPoints.Visibility = ViewStates.Visible;
        }

        private void HideProfileControls()
        {
            profileImage.Visibility = ViewStates.Invisible;
            profileName.Visibility = ViewStates.Invisible;
            badgeCount.Visibility = ViewStates.Invisible;
            jsPoints.Visibility = ViewStates.Invisible;
        }
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        public void ShowAlert(string str)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(str);
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                Finish();
            });

            //run the alert in UI thread to display in the screen
            RunOnUiThread(() => {
                alert.Show();
            });
        }

        public Bitmap GetRoundedShape(Bitmap scaleBitmapImage)
        {
            int targetWidth = 100;
            int targetHeight = 100;
            Bitmap targetBitmap = Bitmap.CreateBitmap(targetWidth,
                targetHeight, Bitmap.Config.Argb8888);

            Canvas canvas = new Canvas(targetBitmap);
            Android.Graphics.Path path = new Android.Graphics.Path();
            path.AddCircle(((float)targetWidth - 1) / 2,
                ((float)targetHeight - 1) / 2,
                (Math.Min(((float)targetWidth),
                    ((float)targetHeight)) / 2),
                Android.Graphics.Path.Direction.Ccw);

            canvas.ClipPath(path);
            Bitmap sourceBitmap = scaleBitmapImage;
            canvas.DrawBitmap(sourceBitmap,
                new Rect(0, 0, sourceBitmap.Width,
                    sourceBitmap.Height),
                new Rect(0, 0, targetWidth, targetHeight), null);
            return targetBitmap;
        }
    }
}