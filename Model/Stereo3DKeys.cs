using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace Advanced3DVConfig.Model
{
    class Stereo3DKeys
    {
        private RegistryKey _stereo3DKey;

        private readonly List<string> _requestedKeys = new List<string>()
        {
            "EnableWindowedMode", "EnablePersistentStereoDesktop", "MonitorSize", "StereoAdjustEnable", "StereoDefaultOn", "StereoSeparation", 
            "StereoVisionConfirmed", "StereoImageType", "SnapShotQuality", "LaserSightEnabled", 
            //Hotkeys
            "CycleFrustumAdjust", "DeleteConfig", "GammaAdjustLess", "GammaAdjustMore", "GlassesDelayMinus", "GlassesDelayPlus", "RHWAtScreenLess", 
            "RHWAtScreenMore", "RHWLessAtScreenLess", "RHWLessAtScreenMore", "SaveStereoImage", "StereoConvergenceAdjustLess", "StereoConvergenceAdjustMore", 
            "StereoSeparationAdjustLess", "StereoSeparationAdjustMore", "StereoSuggestSettings", "StereoToggle", "StereoToggleMode", "StereoUnsuggestSettings", 
            "ToggleAutoConvergence", "ToggleAutoConvergenceRestore", "ToggleLaserSight", "ToggleMemo", "WriteConfig", 
        };
        private List<Stereo3DRegistryKey> _stereo3DSettings = new List<Stereo3DRegistryKey>();
        public List<Stereo3DRegistryKey> Stereo3DSettings { get { return _stereo3DSettings; }
        }

        public Stereo3DKeys() {
            try {
                GetStereo3DKey();
            }
            catch (Exception exception) {
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

        private void ReadStereo3DKeys() {
            foreach (var reqKey in _requestedKeys)
                _stereo3DSettings.Add(new Stereo3DRegistryKey(reqKey, ReadKeyValue(reqKey)));
        }

        private int ReadKeyValue(string keyToRead) {
            object keyValue = _stereo3DKey.GetValue(keyToRead);
            if (keyValue == null) return 0;
            return (int)keyValue;
        }
        /// <summary>
        /// Saves the current changes to the Windows registry.
        /// </summary>
        /// <returns></returns>
        public void SaveSettingsToRegistry(List<Stereo3DRegistryKey> newSettings) {
            if (newSettings.Any())
            {
                foreach (var setting in newSettings)
                {
                    _stereo3DKey.SetValue(setting.KeyName, setting.KeyValue);
                    _stereo3DSettings.Find(k => k.KeyName == setting.KeyName).KeyValue = setting.KeyValue;
                }
            }
        }

        public void SaveSettingsToFile(string filename)
        {
            var keysToWrite = new string[_stereo3DSettings.Count];
            for (int i = 0; i < keysToWrite.Length; i++)
                keysToWrite[i] = String.Format("{0} - {1}", _stereo3DSettings[i].KeyName,
                    _stereo3DSettings[i].KeyValue);
            File.WriteAllLines(filename, keysToWrite);
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
