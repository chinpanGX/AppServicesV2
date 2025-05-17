using UnityEngine;

namespace UniVerse.ComponentEx.Logger
{
    public static class LoggerEx
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