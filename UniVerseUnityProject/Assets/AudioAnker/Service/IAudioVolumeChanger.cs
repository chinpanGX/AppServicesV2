using AudioAnker.Core.AudioAnker.Core;

namespace AudioAnker.Service
{
    public interface IAudioVolumeChanger
    {
        public void ChangeMasterVolume(float volume);
        public void ChangeBgmVolume(float volume);
        public void ChangeSeVolume(float volume);

        public void Save();
        
        public AudioVolumeEntity GetVolume();
    }
}