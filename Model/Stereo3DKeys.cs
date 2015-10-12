using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace Advanced3DVConfig.Model
{
    class Stereo3DKeys
    {
        private RegistryKey _stereo3DKey;

        private readonly List<string> _requestedKeys = new List<string>()
        {
            "EnableWindowedMode", "EnablePersistentStereoDesktop", "MonitorSize", "StereoAdjustEnable",
            "StereoDefaultOn", "StereoSeparation", "StereoVisionConfirmed", "StereoImageType",
            "SnapShotQuality", "LaserSightEnabled", "InterleavePattern0", "InterleavePattern1",
            //Hotkeys
            "CycleFrustumAdjust", "DeleteConfig", "GammaAdjustLess", "GammaAdjustMore", "GlassesDelayMinus",
            "GlassesDelayPlus", "RHWAtScreenLess", "RHWAtScreenMore", "RHWLessAtScreenLess", "RHWLessAtScreenMore",
            "SaveStereoImage", "StereoConvergenceAdjustLess", "StereoConvergenceAdjustMore", "StereoSeparationAdjustLess",
            "StereoSeparationAdjustMore", "StereoSuggestSettings", "StereoToggle", "StereoToggleMode", "StereoUnsuggestSettings", 
            "ToggleAutoConvergence", "ToggleAutoConvergenceRestore", "ToggleLaserSight", "ToggleMemo", "WriteConfig", 
        };
        private readonly List<Stereo3DRegistryKey> _stereo3DSettings = new List<Stereo3DRegistryKey>();
        public List<Stereo3DRegistryKey> Stereo3DSettings { get { return _stereo3DSettings; }}
        public bool SystemSetWriteDenied { get; private set; }

        private readonly RegistryAccessRule _denyWrite = new RegistryAccessRule(
            new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), 
            RegistryRights.Delete | RegistryRights.SetValue, InheritanceFlags.None, 
            PropagationFlags.None, AccessControlType.Deny);
        /// <summary>
        /// Instantiates a new object to read and write NVIDIA 3D Vision registry keys
        /// </summary>
        public Stereo3DKeys() {
            try {
                GetStereo3DKey();
            }
            catch (Exception exception) {
                throw new Exception(exception.Message);
            }
            ReadStereo3DKeys();
            var accessRules = _stereo3DKey.GetAccessControl().
                GetAccessRules(true, true, typeof(SecurityIdentifier));
            CheckSystemAccessRules(accessRules);
        }


        /// <summary>
        /// Saves the current changes to the Windows registry.
        /// </summary>
        /// <returns></returns>
        public void SaveSettingsToRegistry(List<Stereo3DRegistryKey> newSettings)
        {
            if (!newSettings.Any()) return;
            foreach (var setting in newSettings)
            {
                _stereo3DKey.SetValue(setting.KeyName, (int)setting.KeyValue, RegistryValueKind.DWord);
                _stereo3DSettings.Find(k => k.KeyName == setting.KeyName).KeyValue = setting.KeyValue;
            }
        }

        public void ToggleSystemKeyPermissions()
        {
            var regSec = _stereo3DKey.GetAccessControl();
            var accessRules = regSec.GetAccessRules(true, true, typeof(SecurityIdentifier));
            CheckSystemAccessRules(accessRules);
            if (SystemSetWriteDenied)
                regSec.RemoveAccessRule(_denyWrite);
            else
                regSec.AddAccessRule(_denyWrite);
            _stereo3DKey.SetAccessControl(regSec);
        }

        private void CheckSystemAccessRules(AuthorizationRuleCollection accessRules)
        {
            for (int i = 0; i < accessRules.Count & !SystemSetWriteDenied; i++)
            {
                var currentRule = accessRules[i] as RegistryAccessRule;
                if ((currentRule.IdentityReference == _denyWrite.IdentityReference) &&
                    (currentRule.RegistryRights == _denyWrite.RegistryRights))
                {
                    SystemSetWriteDenied = true;
                }
            }
        }

        /// <summary>
        /// Saves all keys to a text file.
        /// </summary>
        /// <param name="filename">Location to save the text file</param>
        public void SaveSettingsToFile(string filename)
        {
            var keysToWrite = new string[_stereo3DSettings.Count];
            for (int i = 0; i < keysToWrite.Length; i++)
                keysToWrite[i] = String.Format("{0} - {1}", _stereo3DSettings[i].KeyName,
                    _stereo3DSettings[i].KeyValue);
            File.WriteAllLines(filename, keysToWrite);
        }

        private void GetStereo3DKey()
        {
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
            foreach (var reqKey in _requestedKeys)
                _stereo3DSettings.Add(new Stereo3DRegistryKey(reqKey, ReadKeyValue(reqKey)));
        }

        private uint ReadKeyValue(string keyToRead)
        {
            object keyValue = _stereo3DKey.GetValue(keyToRead);
            if (keyValue == null)
            {
                keyValue = Stereo3DRegistryKeyDefaults.GetDefaultKeyValue(keyToRead);
                _stereo3DKey.SetValue(keyToRead, keyValue);
            }
            int keyValueInt = Convert.ToInt32(keyValue);
            return unchecked((uint)keyValueInt);
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

        private Dictionary<string, uint> GetAllStereo3DValues()
        {
            var allValueNames = _stereo3DKey.GetValueNames();
            var allValues = new Dictionary<string, uint>();
            foreach (var valueName in allValueNames)
            {
                var value = _stereo3DKey.GetValue(valueName);
                if (value != null)
                    allValues.Add(valueName, (uint)value);
            }
            return allValues;
        }
    }
}
