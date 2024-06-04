using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaImaginarium
{
    // analog to Unity's PlayerPreferences: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
    public static class UserPreferences
    {
        public static Dictionary<string, string> Preferences { get; private set; } = new Dictionary<string, string>();

        public static void SetString(string key, string value)
        {
            Preferences[key] = value;
        }
        public static string? GetString(string key, string defaultValue)
        {
            if (Preferences.TryGetValue(key, out string value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
