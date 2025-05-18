using System;
using UnityEngine;
using UnityEngine.Playables;

namespace UniVerse.ComponentEx
{
    [Serializable]
    public class PlayableAssetGroup
    {
        [SerializeField] private string key;
        [SerializeField] private PlayableAsset playableAsset;

        public string Key => key;
        public PlayableAsset PlayableAsset => playableAsset;

        public PlayableAsset GetPlayableAssetByKey(string request)
        {
            return request == key ? playableAsset : null;
        }
    }
}