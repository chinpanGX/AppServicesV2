using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnVerse.Core
{
    public interface IView
    {
        Canvas Canvas { get; }
        UniTask OpenAsync();
        UniTask CloseAsync();
    }
}