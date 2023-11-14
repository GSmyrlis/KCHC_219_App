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
                new Artist { PhotoPath = "SOR.jpg", Name = "Swarm Of Rats", Description = "Katerinian HC band" },
                new Artist { PhotoPath = "SAD.jpg", Name = "S.A.D.", Description = "A creation of destruction and pain" },
                new Artist { PhotoPath = "Libys.jpg", Name = "Libys", Description = "Katerinian Indie Star" },
                // Add more artists as needed
            };

            BindingContext = Artists;
        }
    }
}
