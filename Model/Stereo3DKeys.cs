//#define DEBUG

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Advanced3DVConfig.Model
{
    class Stereo3DKeys
    {
        private RegistryKey _stereo3DKey;

        public static readonly Dictionary<string, int> KeySettingsDefaults = new Dictionary<string, int>()
        {
            {"EnableWindowedMode", 5}, {"EnablePersistentStereoDesktop", 0}, {"MonitorSize", -1}, {"StereoAdjustEnable", 1}, 
                {"StereoDefaultOn", 1}, {"StereoSeparation", 15},  {"StereoVisionConfirmed", 0},
            {"StereoImageType", 0}, {"SnapShotQuality", 50},
            {"LaserSightEnabled", 1},
        };
        public static readonly Dictionary<string,int>  KeySettingsHotkeyDefaults = new Dictionary<string, int>()
        {
            {"CycleFrustumAdjust", 634}, {"DeleteConfig", 1142}, {"GammaAdjustLess", 1095}, {"GammaAdjustMore", 583}, {"GlassesDelayMinus",1213}, {"GlassesDelayPlus", 1211}, 
            {"RHWAtScreenLess", 632}, {"RHWAtScreenMore", 633}, {"RHWLessAtScreenLess", 1144}, {"RHWLessAtScreenMore", 1145}, {"SaveStereoImage", 1136},
            {"StereoConvergenceAdjustLess", 628}, {"StereoConvergenceAdjustMore", 629}, {"StereoSeparationAdjustLess", 626}, {"StereoSeparationAdjustMore", 627}, 
            {"StereoSuggestSettings", 625}, {"StereoToggle", 596}, {"StereoToggleMode", 1658}, {"StereoUnsuggestSettings", 1137}, {"ToggleAutoConvergence", 631}, 
            {"ToggleAutoConvergenceRestore", 1143}, {"ToggleLaserSight", 635}, {"ToggleMemo", 3629}, {"WriteConfig", 630},
        };

        public Dictionary<string, int> Stereo3DSettings = new Dictionary<string, int>();
        private Dictionary<string, int> _previousStereo3DSettings = new Dictionary<string, int>();
        public Dictionary<string, int> Stereo3DHotkeySettings = new Dictionary<string, int>();
        private Dictionary<string, int> _previousStereo3DHotkeySettings = new Dictionary<string, int>();

        public Stereo3DKeys() {
            try {
                GetStereo3DKey();
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }

            ReadStereo3DKeys();
            _previousStereo3DSettings = new Dictionary<string, int>(Stereo3DSettings);
            _previousStereo3DHotkeySettings = new Dictionary<string, int>(Stereo3DHotkeySettings);
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
            foreach (var keyName in KeySettingsDefaults.Keys)
                Stereo3DSettings.Add(keyName, ReadKeyValue(keyName));
            foreach (var keyNameHotkey in KeySettingsHotkeyDefaults.Keys)
                Stereo3DHotkeySettings.Add(keyNameHotkey, ReadKeyValue(keyNameHotkey));
        }

        private int ReadKeyValue(string keyToRead) {
            object keyValue = _stereo3DKey.GetValue(keyToRead);
            if (keyValue == null) return 0;
            return (int)keyValue;
        }
        /// <summary>
        /// Resets the provided registry key to its default value. (MonitorSize has no default value)
        /// </summary>
        /// <param name="keyToReset">The exact name of the registry key to reset to default.</param>
        public void ResetKeyToDefault(string keyToReset) {
            if(KeySettingsDefaults.ContainsKey(keyToReset) && KeySettingsDefaults[keyToReset] >= 0)
                Stereo3DSettings[keyToReset] = KeySettingsDefaults[keyToReset];
            else if (KeySettingsHotkeyDefaults.ContainsKey(keyToReset))
                Stereo3DHotkeySettings[keyToReset] = KeySettingsHotkeyDefaults[keyToReset];
            else
                throw new ArgumentException("There is no known default value for this key.");
        }
        /// <summary>
        /// Saves the current changes to the Windows registry.
        /// </summary>
        /// <returns></returns>
        public string SaveSettingsToRegistry() {
            var savedSettings = new StringBuilder();

            var combinedSettingsDictionary = CombineSettingsDictionaries();
            var combinedPreviousSettingsDictionary = CombinePreviousSettingsDictionaries();

            var settingsToSave = from s in combinedSettingsDictionary
                                 where s.Value != combinedPreviousSettingsDictionary[s.Key]
                                 select s;
            if (settingsToSave.Any())
            {
                foreach (var setting in settingsToSave)
                {
                    _stereo3DKey.SetValue(setting.Key, setting.Value);
                    savedSettings.Append(String.Format("{0}: {1}", setting.Key, setting.Value) + Environment.NewLine);
                }
                _previousStereo3DSettings = new Dictionary<string, int>(Stereo3DSettings);
                _previousStereo3DHotkeySettings = new Dictionary<string, int>(Stereo3DHotkeySettings);
            }
            else 
                savedSettings.Append("No settings have been changed, so nothing was saved.");
#if DEBUG
            DebugWriteAllStereo3DKeys();
#endif
            return savedSettings.ToString();
        }

        public void SaveSettingsToFile(string filename)
        {
            var combinedSettingsDictionary = CombineSettingsDictionaries();

            var keysToWrite = new string[combinedSettingsDictionary.Count];
            for (int i = 0; i < keysToWrite.Length; i++)
                keysToWrite[i] = String.Format("{0} - {1}", combinedSettingsDictionary.Keys.ElementAt(i),
                    combinedSettingsDictionary.Values.ElementAt(i));
            File.WriteAllLines(filename, keysToWrite);
        }

        private Dictionary<string, int> CombineSettingsDictionaries()
        {
            var combinedSettingsDictionary = new Dictionary<string, int>(Stereo3DSettings);
            foreach (var hotkeySetting in Stereo3DHotkeySettings)
                combinedSettingsDictionary.Add(hotkeySetting.Key, hotkeySetting.Value);
            return combinedSettingsDictionary;
        }

        private Dictionary<string, int> CombinePreviousSettingsDictionaries()
        {
            var combinedPreviousSettingsDictionary = new Dictionary<string, int>(_previousStereo3DSettings);
            foreach (var prevHotkeySetting in _previousStereo3DHotkeySettings)
                combinedPreviousSettingsDictionary.Add(prevHotkeySetting.Key, prevHotkeySetting.Value);
            return combinedPreviousSettingsDictionary;
        }

        private void DebugWriteAllStereo3DKeys ()
        {
            string keysfilename = String.Format(@"C:\Stereo3DKeys {0:h-mm-ss}.txt", DateTime.Now);
            var allValues = GetAllStereo3DValues();
            var keysToWrite = new string[allValues.Count];
            for (int i = 0; i < keysToWrite.Length; i++) {
                keysToWrite[i] = String.Format("{0}: {1}", allValues.Keys.ElementAt(i),
                    allValues.Values.ElementAt(i));
            }
            File.WriteAllLines(keysfilename, keysToWrite);
        }

        private Dictionary<string, int> GetAllStereo3DValues()
        {
            var allValueNames = _stereo3DKey.GetValueNames();
            var allValues = new Dictionary<string, int>();
            foreach (var valueName in allValueNames)
            {
                var value = _stereo3DKey.GetValue(valueName);
                if (value != null)
                    allValues.Add(valueName, (int)value);
            }
            return allValues;
        }
    }
}
