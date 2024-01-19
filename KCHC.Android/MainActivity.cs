using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Media;
using Android.OS;

namespace KCHC.Droid
{
    [Activity(Label = "KCHC", Icon = "@drawable/kchc", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        MediaPlayer mediaPlayer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            // Find the resource ID of your MP3 file
            int soundResourceId = Resource.Raw.emo;

            // Initialize MediaPlayer
            mediaPlayer = MediaPlayer.Create(this, soundResourceId);

            // Start playing the MP3
            mediaPlayer.Start();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            mediaPlayer?.Release();
            mediaPlayer = null;

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}