using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace Food_Menu.Storage
{
    class AppStore
    {
        public static void AddValue(string key, string value)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[key] = value;
        }

        public static string GetValue(string key)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            object value;
            localSettings.Values.TryGetValue(key, out value);
            if(value != null)
            {
                return (string)value;
            }
            return null;
        }
    }
}
