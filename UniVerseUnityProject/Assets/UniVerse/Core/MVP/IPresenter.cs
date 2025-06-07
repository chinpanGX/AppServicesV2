using System;
using Cysharp.Threading.Tasks;

namespace UniVerse.Core
{
    public interface IPresenter : IDisposable
    {
        void Tick();
        UniTask PushAsync();
        UniTask PopAsync();
    }
}