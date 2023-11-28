using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KCHC.Models;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCHC
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Artist> Artists { get; set; }

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Initialize and set the Artists collection with your data
            Artists = new ObservableCollection<Artist>
            {
                new Artist { PhotoPath = "Akatalogistoi.jpg", Name="Akatalogistoi", Description = "Katerinian Hardcopunk", IsABand = true},
                new Artist { PhotoPath = "Killathea.jpg", Name="Killathea", Description = "RnB"},
                new Artist { PhotoPath = "Libys.jpg", Name = "Libys", Description = "Indie", ExtraDescription="Μόλις κυκλοφόρησε το δεύτερο άλμπουμ μου ✨ Ένα mixtape που ηχογραφήθηκε αυθόρμητα μέσα σε λιγότερο από έναν μήνα. Το κρατούσα σχεδόν δύο χρόνια από την δημιουργία του. Ποτέ δεν μπορούσα να αποφασίσω με σιγουριά αν μου άρεσε ή όχι, αλλά σίγουρα μπορώ να πω ότι περιγράφει καλά μια περίοδο της ζωής μου.", SpotifyAccountUrl="https://open.spotify.com/artist/7FUxLkwKbV3eoAgLUkzDXo?si=1Ufz3HGHQQaRPJlBr3W6ww&nd=1&dlsi=b53f102b24924a5f", YoutubeAccountUrl="https://www.youtube.com/@Libys", BandCampAccountUrl="https://libys.bandcamp.com/track/ifeellibys-5" },
                new Artist { PhotoPath = "Mariospol.jpg", Name="Marios Pol", Description = "DJ and Laika"},
                new Artist { PhotoPath = "SAD.jpg", Name = "S.A.D.", Description = "Folk country Deathcore", IsABand = true },
                new Artist { PhotoPath = "SOR.jpg", Name = "Swarm Of Rats", Description = "Hardcore", IsABand = true },
                new Artist { PhotoPath = "Spyridwn.JPG", Name = "Spyridon", Description = "Hard Indie Metal"},
                new Artist { PhotoPath = "TaratsaParadeisou.jpg", Name = "Taratsa Paradeisou", Description = "Blues Indie", IsABand = true}
            };
            BindingContext = this;
            InitializeCustomIndicator(Artists.Count);
            ArtistsCarousel.PositionChanged += OnPositionSelected;
        }
        private void InitializeCustomIndicator(int totalItems)
        {
            for (int i = 0; i < totalItems; i++)
            {
                var boxView = new BoxView
                {
                    WidthRequest = 5,
                    HeightRequest = 5,
                    BackgroundColor = Color.Gray,
                    Margin = new Thickness(5, 0)
                };
                CustomIndicator.Children.Add(boxView);
            }
        }
        private void OnPositionSelected(object sender, PositionChangedEventArgs e)
        {
            // Update the custom indicator based on the selected position
            for (int i = 0; i < CustomIndicator.Children.Count; i++)
            {
                CustomIndicator.Children[i].BackgroundColor = (i == e.CurrentPosition) ? Color.DarkRed : Color.DarkGray;
            }
        }


        private async void OnKinimatoramaImageClicked(object sender, EventArgs e)
        {
            if (Application.Current.MainPage?.Navigation != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new KiminatoramaPage());
            }
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            if (sender is Image tappedImage && tappedImage.BindingContext is Models.Artist selectedArtist)
            {
                await Navigation.PushAsync(new ArtistPage(selectedArtist));
            }
        }
    }
}
