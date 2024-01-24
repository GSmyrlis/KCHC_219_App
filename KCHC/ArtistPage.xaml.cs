using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System;
using KCHC.Models;
using KCHC.Interfaces;
using System.Collections.Generic;

namespace KCHC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistPage : ContentPage
    {
        private Models.Artist ShownArtist { get; set; } = new Models.Artist();
        private string selectedSong = string.Empty;

        public ArtistPage()
        {
            InitializeComponent();
        }
        public ArtistPage(Models.Artist artist)
        {
            InitializeComponent();
            ShowPage(artist);
            switch (ShownArtist.Name)
            {
                case "Taratsa Paradeisou":
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("emo.mp3");
                        break;
                    }
                case "Pizza Boston":
                    {
                        List<string> artistsongs = new List<string>();
                        artistsongs.Add("PizzaBoston_60K100.mp3");
                        artistsongs.Add("PizzaBoston_Denthaseswseikaneis.mp3");
                        InitializeAudioControls(artistsongs);
                        break; 
                    }

            }

        }
        public static string MillisecondsToTimeString(int milliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
            return $"{(int)timeSpan.TotalMinutes:D2}:{timeSpan.Seconds:D2}";
        }

        // Initialize common audio controls
        private void InitializeAudioControls(List<string> ArtistSongs)
        {
            foreach(string song in ArtistSongs)
            {
                RadioButton songRadioButt = new RadioButton();
                songRadioButt.Content = song;
                songRadioButt.CheckedChanged += (s, e) =>
                {
                    if (songRadioButt.IsChecked)
                    {
                        selectedSong = songRadioButt.Content.ToString();
                    }
                };
                GridForMusic.Children.Add(songRadioButt);
            }

            Button playButton = new Button
            {
                Text = "Play",
                Command = new Command(() => DependencyService.Get<IAudio>().PlayAudioFile(selectedSong))
            };

            
            Button pauseButton = new Button
            {
                Text = "Pause",
                Command = new Command(() => DependencyService.Get<IAudio>().PauseAudio())
            };

            Slider positionSlider = new Slider
            {
                Minimum = 0,
                Maximum = DependencyService.Get<IAudio>().GetDuration(),
                Value = 0
            };

            positionSlider.ValueChanged += (sender, e) =>
            {
                DependencyService.Get<IAudio>().SeekTo((int)e.NewValue);
            };

            GridForMusic.Children.Add(playButton);
            GridForMusic.Children.Add(pauseButton);
            GridForMusic.Children.Add(positionSlider);
        }

        // Helper method to create a social media button with an image
        private Button CreateSocialButton(string imageName, string url)
        {
            return new Button
            {
                HeightRequest = 45, // Set the height of the button
                WidthRequest = 45,
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
            
            GridForConnections.Children.Clear();
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

            if (!string.IsNullOrEmpty(artist.SoundcloudAccountUrl))
            {
                Button buttonSoundcloud = CreateSocialButton("soundcloud.png", artist.SoundcloudAccountUrl);
                GridForConnections.Children.Add(buttonSoundcloud, count++, 0);
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