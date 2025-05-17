#nullable enable
using TMPro;
using UnityEngine;

namespace UniVerse.ComponentEx.UI
{
    using Logger;
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CustomText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI? textMeshProUGUI;

        public bool SetTextSafe(string text)
        {
            if (textMeshProUGUI == null)
            {
                LoggerEx.LogError("TextMeshProUGUI is null");
                return false;
            }
            textMeshProUGUI.text = text;
            return true;
        }

        public bool TryGetTextSafe(out string text)
        {
            if (textMeshProUGUI == null)
            {
                LoggerEx.LogError("TextMeshProUGUI is null");
                text = string.Empty;
                return false;
            }
            text = textMeshProUGUI.text;
            return true;
        }

        public bool SetColorSafe(Color color)
        {
            if (textMeshProUGUI == null)
            {
                LoggerEx.LogError("TextMeshProUGUI is null");
                return false;
            }
            textMeshProUGUI.color = color;
            return true;
        }
        
        public bool SetColorSafe(string htmlString)
        {
            if (textMeshProUGUI == null)
            {
                LoggerEx.LogError("TextMeshProUGUI is null");
                return false;
            }
            
            if (!ColorUtility.TryParseHtmlString(htmlString, out var color))
            {
                LoggerEx.LogError($"Invalid color hex: {htmlString}");
                return false;
            }
            
            textMeshProUGUI.color = color;
            return true;
        }
        
        public bool SetAlphaSafe(float alpha)
        {
            if (textMeshProUGUI == null)
            {
                LoggerEx.LogError("TextMeshProUGUI is null");
                return false;
            }
            textMeshProUGUI.alpha = alpha;
            return true;
        }
        
        public bool SetFontSizeSafe(float fontSize)
        {
            if (textMeshProUGUI == null)
            {
                LoggerEx.LogError("TextMeshProUGUI is null");
                return false;
            }
            textMeshProUGUI.fontSize = fontSize;
            return true;
        }
    }
}