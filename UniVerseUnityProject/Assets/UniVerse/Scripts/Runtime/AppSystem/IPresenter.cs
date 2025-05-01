using System;

namespace UniVerse.AppSystem
{
    public interface IPresenter : IDisposable
    {
        void Tick();
    }
}