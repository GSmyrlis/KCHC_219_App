using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace KCHC.Interfaces
{
    public interface IAudio
    {
        void PlayAudioFile(string fileName);
        void PauseAudio();
        double GetDuration();
        int GetCurrentPosition();
        void SeekTo(int position);
        void Reset();
        void PlayFromSpecificTime(string filename, int position);
    }
}
