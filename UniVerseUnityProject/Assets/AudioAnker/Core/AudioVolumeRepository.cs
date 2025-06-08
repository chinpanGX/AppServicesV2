using UnityEngine;

namespace AudioAnker.Core.AudioAnker.Core
{

    public class AudioVolumeRepository
    {
        /// <summary>
        /// PlayerPrefsのキー名
        /// </summary>
        private readonly string masterVolumeKey = "MasterVolume";
        private readonly string bgmVolumeKey = "BgmVolume";
        private readonly string seVolumeKey = "SeVolume";
        
        public AudioVolumeEntity Load()
        {
            var masterVolume = PlayerPrefs.GetFloat(masterVolumeKey, 1f);
            var bgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey, 1f);
            var seVolume = PlayerPrefs.GetFloat(seVolumeKey, 1f);
            return new AudioVolumeEntity(masterVolume, bgmVolume, seVolume);
        }
        
        public void Save(AudioVolumeEntity audioVolumeEntity)
        {
            PlayerPrefs.SetFloat(masterVolumeKey, audioVolumeEntity.MasterVolume);
            PlayerPrefs.SetFloat(bgmVolumeKey, audioVolumeEntity.BgmVolume);
            PlayerPrefs.SetFloat(seVolumeKey, audioVolumeEntity.SeVolume);
            PlayerPrefs.Save();
        }
    }
}