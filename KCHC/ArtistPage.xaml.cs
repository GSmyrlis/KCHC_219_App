using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System;
using KCHC.Models;

namespace KCHC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistPage : ContentPage
    {
        private Models.Artist ShownArtist { get; set; } = new Models.Artist();
        public ArtistPage()
        {
            InitializeComponent();
        }
        public ArtistPage(Models.Artist artist)
        {
            InitializeComponent();
            ShowPage(artist);
        }
        // Helper method to create a social media button with an image
        private Button CreateSocialButton(string imageName, string url)
        {
            return new Button
            {
                HeightRequest = 40, // Set the height of the button
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                ImageSource = imageName,
                Command = new Command(async () =>
                {
                    // Handle button click (e.g., open the URL)
                    if (!string.IsNullOrEmpty(url))
                    {
                        await Launcher.OpenAsync(new Uri(url));
                    }
                })
            };
        }

        public void ShowPage(Models.Artist artist)
        {
            ShownArtist = artist;
            BindingContext = artist;
            ImageArtist.Source = artist.PhotoPath;
            if (artist.DateTime != null && artist.DateTime != System.DateTime.MinValue)
            {
                // Create a new Label for displaying "Created on: "
                Label createdOnLabel = new Label
                {
                    Text = "Created on: " + artist.DateTime.ToString("dd MMM yyyy"),
                    FontSize = 18,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                GridForData.Children.Add(createdOnLabel);
            }

            int count = 0;
            if (!string.IsNullOrEmpty(artist.BandCampAccountUrl))
            {
                Button buttonBandCamp = CreateSocialButton("bandcamp.jpg", artist.BandCampAccountUrl);
                GridForConnections.Children.Add(buttonBandCamp, count++, 0);
            }

            if (!string.IsNullOrEmpty(artist.SpotifyAccountUrl))
            {
                Button buttonSpotify = CreateSocialButton("spotify.jpg", artist.SpotifyAccountUrl);
                GridForConnections.Children.Add(buttonSpotify, count++, 0);
            }

            if (!string.IsNullOrEmpty(artist.YoutubeAccountUrl))
            {
                Button buttonYouTube = CreateSocialButton("youtube.jpg", artist.YoutubeAccountUrl);
                GridForConnections.Children.Add(buttonYouTube, count++, 0);
            }
        }

        private void OnSwipe(object sender, SwipedEventArgs e)
        {
            if (e.Direction == SwipeDirection.Right)
            {
                if (App.Artists.IndexOf(ShownArtist) - 1 >= 0)
                {
                    ShowPage(App.Artists[App.Artists.IndexOf(ShownArtist) - 1]);
                    return;
                }
                ShowPage(App.Artists[App.Artists.Count - 1]);
                return;
            }
            if (e.Direction == SwipeDirection.Left)
            {
                if (App.Artists.IndexOf(ShownArtist) + 1 >= App.Artists.Count)
                {
                    ShowPage(App.Artists[0]);
                    return;
                }
                ShowPage(App.Artists[App.Artists.IndexOf(ShownArtist) + 1]);
                return;
            }
        }
    }
}