using UnityEngine;

namespace AudioAnker.Service
{
    public interface IAudioPlayer
    {
        public void PlayBgm(string clipName);
        public void PlaySe(string clipName);
        public void StopBgm();
    }
}