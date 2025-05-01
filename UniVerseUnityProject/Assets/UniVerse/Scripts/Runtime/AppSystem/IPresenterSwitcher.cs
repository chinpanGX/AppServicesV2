using Cysharp.Threading.Tasks;

namespace UniVerse.AppSystem
{
    public interface IPresenterSwitcher
    {
        UniTask SwitchPresenterAsync(string key);
    }

}