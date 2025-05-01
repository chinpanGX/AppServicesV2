#nullable enable
using UnityEngine;

namespace UniVerse.ComponentEx.UI
{
    using Logger;
    public static class CanvasGroupEx
    {
        public static void SetInteractableSafe(this CanvasGroup? canvasGroup, bool value)
        {
            if (canvasGroup == null)
            {
                LoggerEx.LogError("CanvasGroup is null");
                return;
            }
            canvasGroup.interactable = value;    
        }
        
        public static void SetAlphaSafe(this CanvasGroup? canvasGroup, float value)
        {
            if (canvasGroup == null)
            {
                LoggerEx.LogError("CanvasGroup is null");
                return;
            }
            canvasGroup.alpha = value;    
        }
    }
}