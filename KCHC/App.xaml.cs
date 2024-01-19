using KCHC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace KCHC
{
    public partial class App : Application
    {
        public static ObservableCollection<Models.Artist> Artists = new ObservableCollection<Models.Artist>();
        public App()
        {
            InitializeComponent();
            byte[] resourceBytes = KCHC.Properties.Resources.kchc_artists;
            string jsonFromFile = System.Text.Encoding.UTF8.GetString(resourceBytes);

            // Deserialize from JSON using Json.NET
            Artists = JsonConvert.DeserializeObject<ObservableCollection<Artist>>(jsonFromFile);
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
