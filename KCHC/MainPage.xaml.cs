using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KCHC.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Forms.Xaml;


namespace KCHC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ArtistsCarousel.BindingContext = App.Artists;
            InitializeCustomIndicator(App.Artists.Count);
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
            CustomIndicator.Children[0].BackgroundColor = Color.DarkRed; 
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
