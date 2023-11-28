using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCHC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KiminatoramaPage : ContentPage
    {
        public KiminatoramaPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Set the source of the WebView to the specified URL
            WebVKinimatorama.Source = new UrlWebViewSource
            {
                Url = "https://www.kinimatorama.net/"
            };
        }

    }
}