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
        private int currentPosition;
        public AudioService()
        {
        }
        public void PlayAudioFile(string fileName)
        {
            var context = global::Android.App.Application.Context;
            var resourceId = (int)typeof(Resource.Raw).GetField(fileName)?.GetValue(null);
            var fd = context.Resources.OpenRawResourceFd(resourceId);
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }
            }

            player.Prepared += (s, e) =>
            {
                player.Start();
            };
            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.Prepare();
            // Update the current position during playback
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                if (player.IsPlaying)
                {
                    currentPosition = player.CurrentPosition;
                    return true;
                }
                return false;
            });
        }
        public void PlayFromSpecificTime(string fileName, int startPosition)
        {
            var context = global::Android.App.Application.Context;
            var resourceId = (int)typeof(Resource.Raw).GetField(fileName)?.GetValue(null);
            var fd = context.Resources.OpenRawResourceFd(resourceId);

            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }
                player.Reset();
            }

            player.Prepared += (s, e) =>
            {
                player.Start();
                player.SeekTo(startPosition);
            };

            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.PrepareAsync();
        }

        public void Reset()
        {
            player.Reset();
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
        public int GetCurrentPosition()
        {
            return currentPosition;
        }
    }
}