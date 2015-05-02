using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced3DVConfig.Model
{
    public static class Stereo3DRegistryKeyDefaults
    {
        private static readonly Dictionary<string, int> defaultsDictionary = new Dictionary<string, int>()
        {
            //Settings
            {"EnableWindowedMode", 5}, {"EnablePersistentStereoDesktop", 0}, {"MonitorSize", -1}, {"StereoAdjustEnable", 1}, 
                {"StereoDefaultOn", 1}, {"StereoSeparation", 15},  {"StereoVisionConfirmed", 0},
            {"StereoImageType", 0}, {"SnapShotQuality", 50}, {"LaserSightEnabled", 1}, {"StereoAdvancedHKConfig", 0}, 
            //KeyBindings
            {"CycleFrustumAdjust", 634}, {"DeleteConfig", 1142}, {"GammaAdjustLess", 1095}, {"GammaAdjustMore", 583}, {"GlassesDelayMinus",1213}, {"GlassesDelayPlus", 1211}, 
            {"RHWAtScreenLess", 632}, {"RHWAtScreenMore", 633}, {"RHWLessAtScreenLess", 1144}, {"RHWLessAtScreenMore", 1145}, {"SaveStereoImage", 1136},
            {"StereoConvergenceAdjustLess", 628}, {"StereoConvergenceAdjustMore", 629}, {"StereoSeparationAdjustLess", 626}, {"StereoSeparationAdjustMore", 627}, 
            {"StereoSuggestSettings", 625}, {"StereoToggle", 596}, {"StereoToggleMode", 1658}, {"StereoUnsuggestSettings", 1137}, {"ToggleAutoConvergence", 631}, 
            {"ToggleAutoConvergenceRestore", 1143}, {"ToggleLaserSight", 635}, {"ToggleMemo", 3629}, {"WriteConfig", 630},
            //Unknown
            {"ControlPanel", 0}, {"DrsEnable", 1}, {"GlassesSwitchDelay", 0}, {"LaserSightIndex", 0}, {"ShowAllViewerTypes", 1}, {"StereoAnaglyphType", 1}, 
            {"StereoDefaultONSet", 1}, {"StereoDisableTnL", 0}, {"StereoRefreshDefaultOn", 0}, {"StereoTypeSet", 1}, {"StereoViewerType", -1}, {"Time", -1},
             {"AnaglyphEnabled", -1}, {"LaserSightProperty", -1}, {"LeftAnaglyphFilter", -1}, {"RightAnaglyphFilter", -1}, {"StereoGamma", -1}, {"StereoMonitorID", -1}, 
        };

        private static readonly List<string> hotkeyKeys = new List<string>()
        {
            "CycleFrustumAdjust", "DeleteConfig", "GammaAdjustLess", "GammaAdjustMore", "GlassesDelayMinus", "GlassesDelayPlus", "RHWAtScreenLess", "RHWAtScreenMore", 
            "RHWLessAtScreenLess", "RHWLessAtScreenMore", "SaveStereoImage", "StereoConvergenceAdjustLess",  "StereoConvergenceAdjustMore",  "StereoSeparationAdjustLess", 
            "StereoSeparationAdjustMore", "StereoSuggestSettings", "StereoToggle", "StereoToggleMode", "StereoUnsuggestSettings", "ToggleAutoConvergence", 
            "ToggleAutoConvergenceRestore", "ToggleLaserSight", "ToggleMemo", "WriteConfig",
        };

        public static int GetDefaultKeyValue(string keyName) {
            if (defaultsDictionary.ContainsKey(keyName))
                return defaultsDictionary[keyName];
            return -1;
        }

        public static bool KeyIsHotkey(string keyName)
        {
            return hotkeyKeys.Contains(keyName);
        }
    }
}
