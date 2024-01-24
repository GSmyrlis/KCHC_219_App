using System;
using Xamarin.Forms;
using KCHC.Droid;
using Android.Media;
using Android.Content.Res;
using KCHC.Interfaces;

[assembly: Dependency(typeof(AudioService))]
namespace KCHC.Droid
{
    public class AudioService : IAudio
    {
        MediaPlayer player = new MediaPlayer();

        public AudioService()
        {
        }

        public void PlayAudioFile(string fileName)
        {
            var context = global::Android.App.Application.Context;
            // Assuming "emo" is a resource in the Raw folder
            var resourceId = context.Resources.GetIdentifier(fileName, "raw", context.PackageName);
            var fd = context.Resources.OpenRawResourceFd(resourceId);

            player.Prepared += (s, e) =>
            {
                player.Start();
            };

            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.Prepare();

        }

        public void PauseAudio()
        {
            player.Pause();
        }

        public double GetDuration()
        {
            return player.Duration;
        }

        public void SeekTo(int position)
        {
            player.SeekTo(position);
        }

    }
}   