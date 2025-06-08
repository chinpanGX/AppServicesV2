using AudioAnker.Core;
using AudioAnker.Core.AudioAnker.Core;
using AudioService.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioAnker.Service
{
    public class AudioService : MonoBehaviour, IAudioPlayer, IAudioVolumeChanger
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource seAudioSource;

        [SerializeField] private string addressableLabelName = "Audio";

        [SerializeField] private bool isSingleton = true;
        private static AudioService instance;

        private AudioLoader audioLoader;
        private AudioMixerHandler audioMixerHandler;

        public async UniTask LoadAllAsyncIfNeed()
        {
            await audioLoader.LoadAsyncByLabel(addressableLabelName);
        }

        public void PlayBgm(string clipName)
        {
            var audioClip = audioLoader.GetAudioClip(clipName);
            if (audioClip == null)
            {
                Debug.LogError($"AudioClip '{clipName}' not found.");
                return;
            }
            if (bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Stop();
            }
            bgmAudioSource.clip = audioClip;
            bgmAudioSource.Play();
        }

        public void StopBgm()
        {
            if (bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Stop();
            }
        }

        public void PlaySe(string clipName)
        {
            var audioClip = audioLoader.GetAudioClip(clipName);
            if (audioClip == null)
            {
                Debug.LogError($"AudioClip {clipName} is Not Found");
                return;
            }

            if (seAudioSource.isPlaying)
            {
                seAudioSource.Stop();
            }

            seAudioSource.clip = audioClip;
            seAudioSource.Play();
        }

        public void ChangeMasterVolume(float masterVolume) => audioMixerHandler.ChangeMasterVolume(masterVolume);
        public void ChangeBgmVolume(float bgmVolume) => audioMixerHandler.ChangeBgmVolume(bgmVolume);
        public void ChangeSeVolume(float seVolume) => audioMixerHandler.ChangeSeVolume(seVolume);
        public void Save() => audioMixerHandler.Save();
        public AudioVolumeEntity GetVolume() => audioMixerHandler.GetVolume();

        void Awake()
        {
            if (isSingleton)
            {
                if (instance != null && instance != this)
                {
                    Destroy(gameObject);
                    return;
                }
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            audioLoader = new AudioLoader();
            audioMixerHandler = new AudioMixerHandler(audioMixer);
        }

        void OnDestroy()
        {
            if (isSingleton)
            {
                audioLoader.ReleaseAll();
            }
        }

        private class AudioMixerHandler
        {
            private readonly AudioMixer audioMixer;
            private readonly AudioVolumeRepository repository = new();
            private readonly AudioVolumeEntity audioVolumeEntity;

            /// <summary>
            ///  AudioMixerのExposedパラメータ名
            /// </summary>
            private readonly string masterExposedName = "Master";
            private readonly string bgmExposedName = "Bgm";
            private readonly string seExposedName = "Se";

            internal AudioMixerHandler(AudioMixer audioMixer)
            {
                this.audioMixer = audioMixer;

                audioVolumeEntity = repository.Load();
                audioMixer.SetFloat(masterExposedName, VolumeConverter.ToDecibel(audioVolumeEntity.MasterVolume));
                audioMixer.SetFloat(bgmExposedName, VolumeConverter.ToDecibel(audioVolumeEntity.BgmVolume));
                audioMixer.SetFloat(seExposedName, VolumeConverter.ToDecibel(audioVolumeEntity.SeVolume));
            }

            internal void ChangeMasterVolume(float masterVolume)
            {
                audioVolumeEntity.ChangeMasterVolume(masterVolume);
                audioMixer.SetFloat(masterExposedName, VolumeConverter.ToDecibel(audioVolumeEntity.MasterVolume));
            }

            internal void ChangeBgmVolume(float bgmVolume)
            {
                audioVolumeEntity.ChangeBgmVolume(bgmVolume);
                audioMixer.SetFloat(bgmExposedName, VolumeConverter.ToDecibel(audioVolumeEntity.BgmVolume));
            }

            internal void ChangeSeVolume(float seVolume)
            {
                audioVolumeEntity.ChangeSeVolume(seVolume);
                audioMixer.SetFloat(seExposedName, VolumeConverter.ToDecibel(audioVolumeEntity.SeVolume));
            }

            internal void Save()
            {
                repository.Save(audioVolumeEntity);
            }
            
            internal AudioVolumeEntity GetVolume()
            {
                return audioVolumeEntity;
            }
        }
    }
}