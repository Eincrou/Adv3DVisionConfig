using System.Collections.Generic;

namespace Advanced3DVConfig.Model
{
    public struct SettingsProfile
    {
        public List<SettingsProfileEntry> KeySettings;
        public SettingsProfile(IEnumerable<Stereo3DRegistryKey> keysList)
        {
            KeySettings = new List<SettingsProfileEntry>();
            foreach (var key in keysList)
            {
                KeySettings.Add(new SettingsProfileEntry(key.KeyName, key.KeyValue));
            }
        }
    }
}
