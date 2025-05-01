using System;
using System.Threading;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace UniVerse.ComponentEx
{
    public static class AsyncOperationHandleEx
    {
        /// <summary>
        /// 指定したCancellationTokenで非同期処理(AsyncOperationHandle)のライフサイクルを管理します。
        /// AsyncOperationHandleが無効な場合は例外をスローします。
        /// </summary>
        /// <typeparam name="T">非同期処理の結果の型。</typeparam>
        /// <param name="self">対象のAsyncOperationHandle。</param>
        /// <param name="cancellationToken">非同期処理をキャンセルするためのトークン。</param>
        /// <returns>元のAsyncOperationHandleを返します。</returns>
        /// <exception cref="InvalidOperationException">AsyncOperationHandleが無効な場合にスローされます。</exception>
        /// <exception cref="OperationCanceledException">キャンセル処理が要求された場合にスローされます。</exception>
        public static AsyncOperationHandle<T> AddTo<T>(this AsyncOperationHandle<T> self,
            CancellationToken cancellationToken)
            where T : class
        {
            if (self.IsValid() == false)
            {
                throw new InvalidOperationException("AsyncOperationHandle is not valid.");
            }

            if (cancellationToken.IsCancellationRequested)
            {
                Release();
                throw new OperationCanceledException("The operation was canceled.");
            }

            cancellationToken.Register(Release);
            return self;

            void Release()
            {
                if (!self.IsValid()) return;

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