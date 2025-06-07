using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniVerse.Core
{
    public interface IView
    {
        Canvas Canvas { get; }
        UniTask OpenAsync();
        UniTask CloseAsync();
    }
}