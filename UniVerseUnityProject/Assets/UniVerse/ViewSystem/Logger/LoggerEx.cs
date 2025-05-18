using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("UniVerse.ViewSystem")]

namespace UniVerse.ViewSystem.Logger
{
    internal static class LoggerEx
    {
        internal static void LogError(string message)
        {
            if (EnableLogger())
            {
                Debug.LogError(message);
            }
        }
        
        internal static void LogWarning(string message)
        {
            if (EnableLogger())
            {
                Debug.LogWarning(message);
            }
        }
        
        private static bool EnableLogger ()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD    
            return true;
#endif
            return false;
        } 
    }
}