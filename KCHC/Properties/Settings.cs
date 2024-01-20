using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KCHC.Properties
{
    public static class Settings
    {
        private const string IntroMusicEnabledKey = "IntroMusicEnabled";
        private const string FavoriteArtistKey = "FavoriteArtist";
        private const string BackgroundImageKey = "BackgroundImage";

        public static bool IntroMusicEnabled
        {
            get => App.Current.Properties.ContainsKey(IntroMusicEnabledKey) ? (bool)App.Current.Properties[IntroMusicEnabledKey] : true; // Default to true if not set
            set => App.Current.Properties[IntroMusicEnabledKey] = value;
        }

        public static Models.Artist FavoriteArtist
        {
            get => App.Current.Properties.ContainsKey(FavoriteArtistKey) ? (Models.Artist)App.Current.Properties[FavoriteArtistKey] : null; // Default to null if not set
            set => App.Current.Properties[FavoriteArtistKey] = value;
        }

        public static string BackgroundImage
        {
            get => App.Current.Properties.ContainsKey(BackgroundImageKey) ? (string)App.Current.Properties[BackgroundImageKey] : string.Empty; // Default to empty string if not set
            set => App.Current.Properties[BackgroundImageKey] = value;
        }
    }
}
