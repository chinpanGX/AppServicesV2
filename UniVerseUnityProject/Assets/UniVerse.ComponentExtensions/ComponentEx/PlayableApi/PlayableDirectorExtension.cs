using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace UniVerse.ComponentEx
{
    public static class PlayableDirectorExtensions
    {
        public static async UniTask PlayAsync(this PlayableDirector playableDirector, CancellationToken token)
        {
            if (playableDirector != null)
            {
                Debug.LogError("PlayableDirector is null.");
                return;
            }
            playableDirector.Play();
            await UniTask.WaitUntil(() => playableDirector.duration < playableDirector.time, cancellationToken: token);
        }
    }
}