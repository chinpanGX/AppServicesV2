using Cysharp.Threading.Tasks;

namespace UniVerse.Core
{
    public interface IPresenterSwitcher
    {
        UniTask SwitchPresenterAsync(string key);
    }
}