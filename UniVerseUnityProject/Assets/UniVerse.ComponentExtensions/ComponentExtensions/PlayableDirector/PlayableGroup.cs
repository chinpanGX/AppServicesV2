using System;
using UnityEngine;
using UnityEngine.Playables;

namespace UniVerse.ComponentExtensions
{
    [Serializable]
    public class PlayableGroup
    {
        [SerializeField] private string key;
        [SerializeField] private PlayableAsset playableAsset;

        public string Key => key;
        public PlayableAsset PlayableAsset => playableAsset;

        public PlayableAsset GetPlayableAssetByKey(string key)
        {
            return key == this.key ? playableAsset : null;
        }
    }
}