using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniVerse.AppSystem
{
    public interface IView
    {
        Canvas Canvas { get; }
        UniTask OpenAsync();
        UniTask CloseAsync();
    }
}