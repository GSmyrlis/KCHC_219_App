using KCHC.Properties;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCHC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = typeof(Properties.Settings);
            // Assuming you have a ViewModel for your settings
            // Set the BindingContext to an instance of your ViewModel
            // this.BindingContext = new SettingsViewModel();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            // Handle save button click event here
            // You can access the values using IntroMusicSwitch.IsToggled, FavoriteArtistEntry.Text, BackgroundImageEntry.Text
            // Save the values to your AppSettings or ViewModel
            var introMusicEnabled = Settings.IntroMusicEnabled;
            var favoriteArtist = Settings.FavoriteArtist;
            var backgroundImage = Settings.BackgroundImage;
        }
    }
}
