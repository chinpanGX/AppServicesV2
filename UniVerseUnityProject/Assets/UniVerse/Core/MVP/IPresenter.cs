using System;
using Cysharp.Threading.Tasks;

namespace UnVerse.Core
{
    public interface IPresenter : IDisposable
    {
        void Tick();
        UniTask PushAsync();
        UniTask PopAsync();
    }
}