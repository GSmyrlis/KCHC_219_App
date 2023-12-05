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
                new Artist { PhotoPath = "Akatalogistoi.jpg", Name="Akatalogistoi", Description = "Katerinian Hardcopunk", IsABand = true, YoutubeAccountUrl="https://www.youtube.com/watch?v=dGArrZ8m1MU", ExtraDescription="Spyke-Nesik-Sok Katerinian Hardcopunk band, 2014-2015"},
                new Artist { PhotoPath = "Killathea.jpg", Name="Killathea", Description = "Emotional rap", SpotifyAccountUrl="https://open.spotify.com/artist/38fatBRI4r5wi7VReN7arO", YoutubeAccountUrl="https://www.youtube.com/@killathea307", ExtraDescription="Συναισθηματικό ραπ κατευθείαν από το καζάνι της τρέλας για ερωτικούς, αιρετικούς και τρελούς που είναι τουλάχιστον ευτυχείς." },
                new Artist { PhotoPath = "Libys.jpg", Name = "Libys", Description = "Indie", ExtraDescription="Μόλις κυκλοφόρησε το δεύτερο άλμπουμ μου ✨ Ένα mixtape που ηχογραφήθηκε αυθόρμητα μέσα σε λιγότερο από έναν μήνα. Το κρατούσα σχεδόν δύο χρόνια από την δημιουργία του. Ποτέ δεν μπορούσα να αποφασίσω με σιγουριά αν μου άρεσε ή όχι, αλλά σίγουρα μπορώ να πω ότι περιγράφει καλά μια περίοδο της ζωής μου.", SpotifyAccountUrl="https://open.spotify.com/artist/7FUxLkwKbV3eoAgLUkzDXo?si=1Ufz3HGHQQaRPJlBr3W6ww&nd=1&dlsi=b53f102b24924a5f", YoutubeAccountUrl="https://www.youtube.com/@Libys", BandCampAccountUrl="https://libys.bandcamp.com/track/ifeellibys-5" },
                new Artist { PhotoPath = "Mariospol.jpg", Name="MayDay Music", Description = "DJ and Laika", ExtraDescription="MariosPol", YoutubeAccountUrl="https://www.youtube.com/@MariosDimitriou", SpotifyAccountUrl="https://open.spotify.com/user/11120416820", BandCampAccountUrl="https://maydayofc.bandcamp.com/" },
                new Artist { PhotoPath = "SAD.jpg", Name = "S.A.D.", Description = "Folk country Deathcore", IsABand = true, ExtraDescription="Smileumenoi sta skotina ghetto tis elladas kai exontas epizisi polles aimatires siggrousis me ta tsonia apofasisame na peksoume rok mousiki", BandCampAccountUrl="https://sadsxolh.bandcamp.com/", YoutubeAccountUrl="https://www.youtube.com/@TheSmirlis", SpotifyAccountUrl="https://open.spotify.com/artist/64D1b2JIB1pFXNsGeDzbbr" },
                new Artist { PhotoPath = "SOR.jpg", Name = "Swarm Of Rats", Description = "Hardcore", IsABand = true, SpotifyAccountUrl="https://swarmofratshc219.bandcamp.com/", BandCampAccountUrl="https://www.youtube.com/channel/UCnmh1PSVs-zCLSa5MCZOldg", ExtraDescription="A 4 piece hardcore band from Katerini est. 2014 Gio-Vocals George-Drums Soc-Guitar Chris-Bass K.CITY HARDCORE. -219-"},
                new Artist { PhotoPath = "Spyridwn.JPG", Name = "Spyridon", Description = "Hard Indie Metal", SpotifyAccountUrl="https://open.spotify.com/artist/12PgN4K4tut8SunQ9fBn76", ExtraDescription="Dark hard indie Metal for those who are chosen. Created by Spyros Kreator"},
                new Artist { PhotoPath = "TaratsaParadeisou.jpg", Name = "Taratsa Paradeisou", Description = "Blues Indie", ExtraDescription="No Extra Description is required for Taratsa Paradeisou", IsABand = true, SpotifyAccountUrl="https://open.spotify.com/artist/6mxsIAUaOuuaYYVSkQ0xM9", YoutubeAccountUrl="https://www.youtube.com/channel/UCzNawIaqEx0rQ87S8v5uYRg" }
                //new Artist { PhotoPath = "MauraTsigara.jpg", Name="Maura Tsigara"}
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
