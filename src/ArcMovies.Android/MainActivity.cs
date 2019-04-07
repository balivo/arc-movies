
using Android.App;
using Android.Content.PM;
using Android.OS;
using XamarinForms = Xamarin.Forms.Forms;
using XamarinEssentialsPlatform = Xamarin.Essentials.Platform;
using Android.Runtime;
using Xamarin.Forms.Platform.Android;

namespace ArcMovies.Droid
{
    [Activity(
        Label = "ArcMovies"
        , Icon = "@mipmap/icon"
        , Theme = "@style/MainTheme"
        , MainLauncher = true
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.ScreenSize
        )]
    public class MainActivity : FormsAppCompatActivity
    {
        public static MainActivity Current { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            XamarinEssentialsPlatform.Init(this, savedInstanceState);
            XamarinForms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Current = this;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            XamarinEssentialsPlatform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}