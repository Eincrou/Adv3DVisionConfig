#define DEBUG

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Advanced3DVConfig.Model
{
    class Stereo3DKeys
    {
        private RegistryKey _stereo3DKey;

        private readonly Dictionary<string, int> _keySettingsDefaults = new Dictionary<string, int>()
        {
            {"EnableWindowedMode", 5}, {"EnablePersistentStereoDesktop", 0}, {"MonitorSize", -1}, {"StereoAdjustEnable", 1}, 
                {"StereoDefaultOn", 1}, {"StereoSeparation", 15},  {"StereoVisionConfirmed", 0},
            {"SaveStereoImage", 1136}, {"StereoImageType", 0}, {"SnapShotQuality", 50},
            {"LaserSightEnabled", 1},
        };

        public Dictionary<string, int> Stereo3DSettings = new Dictionary<string, int>();
        private Dictionary<string, int> _previousStereo3DSettings = new Dictionary<string, int>();
        public Stereo3DKeys() {
            try {
                GetStereo3DKey();
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }

            ReadStereo3DKeys();
            _previousStereo3DSettings = new Dictionary<string, int>(Stereo3DSettings);
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

        private void ReadStereo3DKeys() {
            foreach (var keyName in _keySettingsDefaults.Keys)
                Stereo3DSettings.Add(keyName, ReadKeyValue(keyName));
        }

        private int ReadKeyValue(string keyToRead) {
            object keyValue = _stereo3DKey.GetValue(keyToRead);
            if (keyValue == null) return 0;
            return (int)keyValue;
        }

        public void ResetKeyToDefault(string keyToReset) {
            if (!_keySettingsDefaults.ContainsKey(keyToReset) | _keySettingsDefaults[keyToReset] < 0)
                throw new ArgumentException("There is no known default value for this key.");
            int defaultValue = _keySettingsDefaults[keyToReset];
            Stereo3DSettings[keyToReset] = defaultValue;
        }

        public string SaveSettingsToRegistry() {
            var savedSettings = new StringBuilder();
            var settingsToSave = from s in Stereo3DSettings
                                 where s.Value != _previousStereo3DSettings[s.Key]
                                 select s;
            foreach (var setting in settingsToSave) {
                _stereo3DKey.SetValue(setting.Key, setting.Value);
                savedSettings.Append(String.Format("{0}: {1}", setting.Key, setting.Value) + Environment.NewLine);
            }
            _previousStereo3DSettings = new Dictionary<string, int>(Stereo3DSettings);
#if DEBUG
            DebugWriteAllStereo3DKeys();
#endif
            return savedSettings.ToString();
        }

        private void DebugWriteAllStereo3DKeys ()
        {
            string keysfilename = String.Format(@"C:\Stereo3DKeys {0:h-mm-ss}.txt", DateTime.Now);
            var allValueNames = _stereo3DKey.GetValueNames();
            var allValues = new Dictionary<string, string>();
            foreach (var valueName in allValueNames) {
                var value = _stereo3DKey.GetValue(valueName);
                if (value != null)
                    allValues.Add(valueName, value.ToString());
            }
            var keysToWrite = new string[allValues.Count];
            for (int i = 0; i < keysToWrite.Length; i++) {
                keysToWrite[i] = String.Format("{0}: {1}", allValues.Keys.ElementAt(i),
                    allValues.Values.ElementAt(i));
            }
            File.WriteAllLines(keysfilename, keysToWrite);
        }
    }
}
