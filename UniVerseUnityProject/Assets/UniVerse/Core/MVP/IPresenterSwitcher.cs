using Cysharp.Threading.Tasks;

namespace UnVerse.Core
{
    public interface IPresenterSwitcher
    {
        UniTask SwitchPresenterAsync(string key);
    }

}