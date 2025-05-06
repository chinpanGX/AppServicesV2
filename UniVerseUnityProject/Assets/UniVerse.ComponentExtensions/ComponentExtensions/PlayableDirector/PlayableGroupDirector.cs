using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace UniVerse.ComponentExtensions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class PlayableGroupDirector : MonoBehaviour
    {
        [SerializeField] private PlayableGroup[] playableGroups;
        [SerializeField] private PlayableDirector playableDirector;

        private Dictionary<string, PlayableAsset> playableAssets = new();

        private void Awake()
        {
            if (playableGroups.Length == 0)
            {
                Debug.LogError("No playable groups assigned.");
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
                Debug.LogError($"Playable asset with key '{key}' not found.");
                return;
            }
            
            playableDirector.playableAsset = playableAsset;
            await playableDirector.PlayAsync(token);
        }
    }
}