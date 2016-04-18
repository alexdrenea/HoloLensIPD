using Windows.Storage;

namespace HoloLensIPD.Helpers
{
    public static class ApplicationDataHelpers
    {

        public static T GetLocalValue<T>(string key)
        {
            return GetValue<T>(key, ApplicationData.Current.LocalSettings);
        }

        public static T GetRoamingValue<T>(string key)
        {
            return GetValue<T>(key, ApplicationData.Current.RoamingSettings);
        }

        public static void SetLocalValue<T>(string key, T value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        public static void SetRoamingValue<T>(string key, T value)
        {
            ApplicationData.Current.RoamingSettings.Values[key] = value;
        }

        private static T GetValue<T>(string key, ApplicationDataContainer source)
        {
            object value = source.Values[key];

            if (value == null)
            {
                return default(T);
            }
            else
            {
                return (T)value;
            }
        }
    }
}
