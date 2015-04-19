using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Advanced3DVConfig.Model
{
    class Stereo3DKeys
    {
        private RegistryKey _stereo3DKey;

        private readonly Dictionary<string, int> _keySettingsDefaults = new Dictionary<string, int>()
        {
            {"StereoSeparation", 15}, {"MonitorSize", 44},
            {"SaveStereoImage", 1136}, {"StereoImageType", 0}, {"SnapShotQuality", 50}
        };

        public Dictionary<string, int> Stereo3DSettings = new Dictionary<string, int>();
        public Stereo3DKeys()
        {
            try{
                GetStereo3DKey();
            }
            catch (Exception exception){
                throw new Exception(exception.Message);
            }

            ReadStereo3DKeys();
        }

        private void GetStereo3DKey(){
            RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software");
            if (softwareKey == null)
                throw new Exception("Registry could not be accessed.");
            RegistryKey nvidiaKey = softwareKey.OpenSubKey("NVIDIA Corporation");
            if (nvidiaKey == null)
                throw new Exception("Nvidia drivers not installed");
            RegistryKey globalKey = nvidiaKey.OpenSubKey("Global");
            if (globalKey == null)
                throw new Exception("Nvidia global settings could not be read.");
            RegistryKey stereo3DKey = globalKey.OpenSubKey("Stereo3D", true);
            if (stereo3DKey == null)
                throw new Exception("3D Vision stereo driver settings could not be read.");
            _stereo3DKey = stereo3DKey;
        }

        private void ReadStereo3DKeys()
        {
            foreach (var keyName in _keySettingsDefaults.Keys)
                Stereo3DSettings.Add(keyName, ReadKeyValue(keyName));
        }

        private int ReadKeyValue(string keyToRead)
        {
            object keyValue = _stereo3DKey.GetValue(keyToRead);
            if (keyValue == null) return 0;
            return (int) keyValue;
        }

        public void ResetKeyToDefault(string keyToReset)
        {
            if(!_keySettingsDefaults.ContainsKey(keyToReset))
                throw new ArgumentException("There is no default value for this key.");
            int defaultValue = _keySettingsDefaults[keyToReset];
            Stereo3DSettings[keyToReset] = defaultValue;
        }

        public void SaveSettingsToRegistry()
        {
            foreach (var setting in Stereo3DSettings)
            {
                _stereo3DKey.SetValue(setting.Key, setting.Value);
            }
            
        }
    }
}
