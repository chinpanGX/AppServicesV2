using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Playables;

namespace UniVerse.ComponentEx
{
    using Logger;
    public static class PlayableDirectorEx
    {
        public static async UniTask PlayAsync(this PlayableDirector playableDirector, CancellationToken token)
        {
            if (playableDirector != null)
            {
                LoggerEx.LogError("PlayableDirector is null");
                return;
            }
            playableDirector.Play();
            await UniTask.WaitUntil(() => playableDirector.duration < playableDirector.time, cancellationToken: token);
        }
    }
}