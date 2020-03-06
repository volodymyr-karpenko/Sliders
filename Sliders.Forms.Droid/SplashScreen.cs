using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using Sliders.Forms.UI;
using System.Threading.Tasks;

namespace Sliders.Forms.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Landscape)]
    public class SplashScreen : MvxFormsSplashScreenActivity<MvxFormsAndroidSetup<Core.App, FormsApp>, Core.App, FormsApp>
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            Xamarin.Essentials.Platform.Init(this, bundle);
            StartActivity(typeof(MainActivity));
            return base.RunAppStartAsync(bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}