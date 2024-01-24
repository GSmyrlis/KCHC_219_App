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
        private bool isPlaying = false;
        private Button playButton;
        private double lastPlaybackPosition = 0;

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
                        DependencyService.Get<IAudio>().PlayAudioFile("emo");
                        break;
                    }
                case "Pizza Boston":
                    {
                        List<string> artistsongs = new List<string>();
                        artistsongs.Add("PizzaBoston_60K100");
                        artistsongs.Add("PizzaBoston_Denthaseswseikaneis");
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
            int rowCounter = 0;

            foreach (string song in ArtistSongs)
            {
                RadioButton songRadioButt = new RadioButton();
                songRadioButt.Content = song;
                songRadioButt.CheckedChanged += (s, e) =>
                {
                    if (songRadioButt.IsChecked)
                    {
                        selectedSong = songRadioButt.ContentAsString();
                    }
                };

                // Set the row for the radio button
                Grid.SetRow(songRadioButt, rowCounter);

                // Increment the row counter for the next control
                rowCounter++;

                GridForMusic.Children.Add(songRadioButt);
            }

            Slider positionSlider = new Slider
            {
                Minimum = 0,
                Value = 0
            };

            // Set the row for the slider
            Grid.SetRow(positionSlider, rowCounter);

            // Increment the row counter for the next control
            rowCounter++;

            Label durationLabel = new Label
            {
                Text = "0:00", // Initial text, you may update it dynamically
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            // Set the row for the duration label
            Grid.SetRow(durationLabel, rowCounter);

            // Increment the row counter for the next control
            rowCounter++;

            positionSlider.ValueChanged += (sender, e) =>
            {
                DependencyService.Get<IAudio>().SeekTo((int)e.NewValue);

                // Update the duration label based on the slider position
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(e.NewValue);
                durationLabel.Text = $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:D2}";
            };


            playButton = new Button
            {
                Text = "Play",
                Command = new Command(() =>
                {
                    if (!isPlaying)
                    {
                        // Resume playback from the last position if available
                        if (lastPlaybackPosition > 0)
                        {
                            DependencyService.Get<IAudio>().SeekTo((int)lastPlaybackPosition);
                            lastPlaybackPosition = 0; // Reset the last playback position
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile(selectedSong);

                            // Set the Maximum property after starting playback
                            positionSlider.Maximum = DependencyService.Get<IAudio>().GetDuration();
                        }

                        isPlaying = true; // Update the playback state
                        playButton.Text = "Pause"; // Change button text to "Pause"
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PauseAudio();
                        isPlaying = false; // Update the playback state
                        playButton.Text = "Play"; // Change button text to "Play"

                        // Store the current playback position when pausing
                        lastPlaybackPosition = positionSlider.Value;
                    }
                })
            };

            // Set the row for the play button
            Grid.SetRow(playButton, rowCounter);

            // Increment the row counter for the next control
            rowCounter++;

            Button pauseButton = new Button
            {
                Text = "Pause",
                Command = new Command(() => DependencyService.Get<IAudio>().PauseAudio())
            };

            // Set the row for the pause button
            Grid.SetRow(pauseButton, rowCounter);

            // Increment the row counter for the next control
            rowCounter++;

            GridForMusic.Children.Add(positionSlider);
            GridForMusic.Children.Add(durationLabel);
            GridForMusic.Children.Add(playButton);
            GridForMusic.Children.Add(pauseButton);
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