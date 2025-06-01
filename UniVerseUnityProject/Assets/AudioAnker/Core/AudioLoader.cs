using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AudioAnker.Core
{
    public class AudioLoader
    {
        private readonly Dictionary<string, AsyncOperationHandle<AudioClip>> audioClipHandles;

        public AudioLoader(int initialCapacity = 10)
        {
            audioClipHandles = new Dictionary<string, AsyncOperationHandle<AudioClip>>(initialCapacity);
        }
        
        /// <summary>
        /// 指定されたAudioClip名に対応するAudioClipを取得します。
        /// </summary>
        /// <param name="audioClipName">取得したいAudioClipの名前。</param>
        /// <returns>指定されたAudioClipを返します。AudioClipが存在しない場合はnullを返します。</returns>
        public AudioClip GetAudioClip(string audioClipName)
        {
            if (audioClipHandles.TryGetValue(audioClipName, out var handle))
            {
                return handle.Result;
            }

            Debug.LogError($"AudioClip not found: {audioClipName}");
            return null;
        }

        /// <summary>
        /// 指定されたラベルに基づいてすべてのAudioClipを非同期にロードします。
        /// </summary>
        /// <returns>ロードの完了を通知するUniTaskを返します。ロード中にエラーが発生した場合はログにエラーメッセージが出力されます。</returns>
        public async UniTask LoadAsyncByLabel(string addressableLabelName)
        {
            try
            {
                var locationsHandle = Addressables.LoadResourceLocationsAsync(addressableLabelName);
                await locationsHandle.ToUniTask();

                var handles = locationsHandle.Result
                    .Select(Addressables.LoadAssetAsync<AudioClip>)
                    .ToList();
                
                await UniTask.WhenAll(handles.Select(x => x.ToUniTask()));
                
                foreach (var handle in handles.Where(handle => !audioClipHandles.TryAdd(handle.Result.name, handle)))
                {
                    Debug.LogWarning($"Duplicate key detected: {handle.Result.name}");
                }
                
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load AudioClip by label '{addressableLabelName}': {e.Message}");
            }
        }

        /// <summary>
        /// 指定されたキーに基づいてAudioClipを非同期にロードします。
        /// </summary>
        /// <param name="addressableKey">ロードするAudioClipのAddressableキー。</param>
        /// <returns>ロードの完了を通知するUniTaskを返します。ロード中にエラーが発生した場合はログにエラーメッセージが出力されます。</returns>
        public async UniTask LoadAsyncByKey(string addressableKey)
        {
            try
            {
                var handle = Addressables.LoadAssetAsync<AudioClip>(addressableKey);
                await handle.ToUniTask();
                
                if (!audioClipHandles.TryAdd(handle.Result.name, handle))
                {
                    Debug.LogWarning($"Duplicate key detected: {handle.Result.name}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load AudioClip by key '{addressableKey}': {e.Message}");
            }
        }

        /// <summary>
        /// 指定されたAudioClip名に基づいてロードされたAudioClipを解放します。
        /// </summary>
        /// <param name="audioClipName">解放したいAudioClipの名前。</param>
        public void Release(string audioClipName)
        {
            if (audioClipHandles.TryGetValue(audioClipName, out var handle))
            {
                Addressables.Release(handle);
                audioClipHandles.Remove(audioClipName);
            }
            else
            {
                Debug.LogWarning($"AudioClip not found for release: {audioClipName}");
            }
        }

        /// <summary>
        /// ロードされたすべてのAudioClipを解放します。
        /// </summary>
        public void ReleaseAll()
        {
            foreach (var handle in audioClipHandles.Values)
            {
                Addressables.Release(handle);
            }
            audioClipHandles.Clear();
        }
    }
}