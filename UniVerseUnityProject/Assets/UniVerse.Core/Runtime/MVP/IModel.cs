using System;

namespace UnVerse.Core
{
    public interface IModel : IDisposable
    {
        void Initialize();
        void Tick();
    }
}