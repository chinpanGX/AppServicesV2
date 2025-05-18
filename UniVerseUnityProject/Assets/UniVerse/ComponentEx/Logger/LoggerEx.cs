using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("UniVerse.ComponentEx.PlayableApi")]
[assembly: InternalsVisibleTo("UniVerse.ComponentEx.UI")]
[assembly: InternalsVisibleTo("UniVerse.ComponentEx.UI.Button")]

namespace UniVerse.ComponentEx.Logger
{
    internal static class LoggerEx
    {
        public static void LogError(string message)
        {
            if (EnableLogger())
            {
                Debug.LogError(message);
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