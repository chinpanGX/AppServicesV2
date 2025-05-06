using System;

namespace UnVerse.Core
{
    public interface IPresenter : IDisposable
    {
        void Tick();
    }
}