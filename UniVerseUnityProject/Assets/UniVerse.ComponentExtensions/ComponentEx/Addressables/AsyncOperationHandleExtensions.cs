using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Cysharp.Threading.Tasks;

namespace UniVerse.ComponentEx
{
    public static class AsyncOperationHandleExtensions
    {
        /// <summary>
        /// AsyncOperationHandleのライフタイムを指定したGameObjectに紐付けます。
        /// </summary>
        /// <param name="self">バインド対象の非同期操作ハンドル。</param>
        /// <param name="gameObject">ハンドルのライフタイムを紐付ける対象のGameObject。</param>
        /// <returns>紐付けられた非同期操作ハンドルを返します。</returns>
        /// <exception cref="ArgumentNullException">gameObjectがnullの場合にスローされる例外。</exception>
        public static AsyncOperationHandle<T> AddTo<T>(this AsyncOperationHandle<T> self, GameObject gameObject)
        {
            if (gameObject == null)
            {
                Release();
                throw new ArgumentNullException(nameof(gameObject));
            }

            var cancellationToken = gameObject.GetCancellationTokenOnDestroy();
            cancellationToken.Register(Release);
            return self;

            void Release()
            {
                if (self.IsValid())
                {
                    if (typeof(T) == typeof(SceneInstance))
                    {
                        Addressables.UnloadSceneAsync(self);
                    }
                    else
                    {
                        Addressables.Release(self);
                    }
                }
            }
        }
    }
}