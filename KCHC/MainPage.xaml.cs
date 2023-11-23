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
            // Initialize and set the Artists collection with your data
            Artists = new ObservableCollection<Artist>
            {
                new Artist {PhotoPath = "Akatalogistoi.jpg", Name="Akatalogistoi", Description = "Katerinian Hardcopunk"},
                new Artist {PhotoPath = "Killathea.jpg", Name="Killathea", Description = "RnB"},
                new Artist { PhotoPath = "Libys.jpg", Name = "Libys", Description = "Indie" },
                new Artist {PhotoPath = "Mariospol.jpg", Name="Marios Pol", Description = "DJ and Laika"},
                new Artist { PhotoPath = "SAD.jpg", Name = "S.A.D.", Description = "Folk country Deathcore" },
                new Artist { PhotoPath = "SOR.jpg", Name = "Swarm Of Rats", Description = "Hardcore" },
                new Artist { PhotoPath = "TaratsaParadeisou.jpg", Name = "Taratsa Paradeisou", Description = "Blues Indie"}
            };
            BindingContext = this;
        }
    }
}
