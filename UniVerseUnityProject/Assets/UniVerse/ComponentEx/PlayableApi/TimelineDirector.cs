using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace UniVerse.ComponentEx
{
    using Logger;
    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineDirector : MonoBehaviour
    {
        [SerializeField] private PlayableAssetGroup[] playableGroups;
        [SerializeField] private PlayableDirector playableDirector;

        private Dictionary<string, PlayableAsset> playableAssets = new();

        private void Awake()
        {
            if (playableGroups.Length == 0)
            {
                LoggerEx.LogError("PlayableGroup is null");
                return;
            }
            
            if (playableDirector == null)
            {
                playableDirector = GetComponent<PlayableDirector>();
            }
            playableAssets = playableGroups.ToDictionary(group => group.Key, group => group.PlayableAsset);
        }
        
        public async UniTask PlayAsync(string key, CancellationToken token)
        {
            if (!playableAssets.TryGetValue(key, out var playableAsset))
            {
                LoggerEx.LogError($"PlayableAsset not found for key: {key}");
                return;
            }
            
            playableDirector.playableAsset = playableAsset;
            await playableDirector.PlayAsync(token);
        }
    }
}