using System;
using System.Collections.Generic;
using System.Text;

namespace KCHC.Interfaces
{
    public interface IAudio
    {
        void PlayAudioFile(string fileName);
        void PauseAudio();
        double GetDuration();
        void SeekTo(int position);
    }
}
