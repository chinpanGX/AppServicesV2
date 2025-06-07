using System;

namespace UniVerse.Core
{
    public interface IModel : IDisposable
    {
        void Initialize();
        void Tick();
    }
}