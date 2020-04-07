using Star8Test.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Star8Test.iOS.Extensions;
using CoreGraphics;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NoLineNavigationRenderer))]
namespace Star8Test.iOS.Renderers
{
    public class NoLineNavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // remove lower border and shadow of the navigation bar
            NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage();
            NavigationBar.BarTintColor = UIColor.Clear.FromHexString("#1D1848", 1.0f);
            NavigationBar.TintColor = UIColor.White;

            NavigationBar.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.White
            };

        }

    }
}
