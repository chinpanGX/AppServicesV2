using System;
using UnityEngine;

namespace UniVerse.ComponentEx
{
    public sealed class AsyncOperationHandleReleaseTrigger : MonoBehaviour
    {
        private Action releaseInternal;
        
        private void OnDestroy()
        {
            releaseInternal?.Invoke();
            releaseInternal = null;
        }
        
        public void Register(Action action)
        {
            releaseInternal = action;
        }
    }
}
