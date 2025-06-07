using Cysharp.Threading.Tasks;
using UnityEngine;
using UniVerse.ComponentEx;
using UniVerse.Core;

namespace UniVerse.ViewSystem
{
    public abstract class AppView : MonoBehaviour, IView
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TimelineDirector timelineDirector;

        public Canvas Canvas => canvas;
        
        protected BaseScreenLayer ScreenLayer { get; set; }
        
        public async UniTask OpenAsync()
        {
            ScreenLayer.Push(this);
            await timelineDirector.PlayAsync("Open", destroyCancellationToken);
        }
        
        public async UniTask CloseAsync()
        {
            ScreenLayer.Pop(this);
            await timelineDirector.PlayAsync("Close", destroyCancellationToken);
            Destroy(gameObject);
        }
    }
}