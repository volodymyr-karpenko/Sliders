using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using Sliders.Core;
using Sliders.Forms.UI;
using UIKit;

namespace Sliders.Forms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<App, FormsApp>, App, FormsApp>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //HttpClient Implementation was changed from NSUrlSession to Managed, you need to revert this change before releasing the app
            //To achieve this, go to your iOS project properties -> iOS Build -> HttpClient Implementation

            //SSL errors can be ignored on iOS for local secure web services
#if DEBUG
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                if (certificate.Issuer.Equals("CN=localhost"))
                    return true;
                return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
            };
#endif

            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(59, 89, 152);
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(255, 255, 255);

            return base.FinishedLaunching(app, options);
        }
    }
}