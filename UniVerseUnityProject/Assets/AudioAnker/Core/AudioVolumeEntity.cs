using UnityEngine;

namespace AudioAnker.Core.AudioAnker.Core
{
    public class AudioVolumeEntity
    {
        public float MasterVolume { get; private set; }
        public float BgmVolume { get; private set; }
        public float SeVolume { get; private set; }

        public AudioVolumeEntity(float masterVolume, float bgmVolume, float seVolume)
        {
            if (masterVolume < 0f || masterVolume > 1f)
                throw new System.ArgumentOutOfRangeException(nameof(masterVolume), "Master volume must be between 0 and 1.");
            if (bgmVolume < 0f || bgmVolume > 1f)
                throw new System.ArgumentOutOfRangeException(nameof(bgmVolume), "BGM volume must be between 0 and 1.");
            if (seVolume < 0f || seVolume > 1f)
                throw new System.ArgumentOutOfRangeException(nameof(seVolume), "SE volume must be between 0 and 1.");
            MasterVolume = masterVolume;
            BgmVolume = bgmVolume;
            SeVolume = seVolume;
        }
        
        public AudioVolumeEntity ChangeMasterVolume(float masterVolume)
        {
            var newMasterVolume = Mathf.Clamp(masterVolume, 0f, 1f);
            MasterVolume = newMasterVolume;
            return this;
        }
        
        public AudioVolumeEntity ChangeBgmVolume(float bgmVolume)
        {
            var newBgmVolume = Mathf.Clamp(bgmVolume, 0f, 1f);
            BgmVolume = newBgmVolume;
            return this;
        }
        
        public AudioVolumeEntity ChangeSeVolume(float seVolume)
        {
            var newSeVolume = Mathf.Clamp(seVolume, 0f, 1f);
            SeVolume = newSeVolume;
            return this;
        }
    }
}