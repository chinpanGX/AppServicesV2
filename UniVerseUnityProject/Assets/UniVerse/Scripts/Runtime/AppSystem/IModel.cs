using System;

namespace UniVerse.AppSystem
{
    public interface IModel : IDisposable
    {
        void Initialize();
        void Tick();
    }
}
