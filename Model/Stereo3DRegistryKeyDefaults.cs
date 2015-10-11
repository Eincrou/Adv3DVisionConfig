using System;
using System.Collections.Generic;

namespace Advanced3DVConfig.Model
{
    public static class Stereo3DRegistryKeyDefaults
    {
        private static readonly Dictionary<string, uint> DefaultsDictionary = new Dictionary<string, uint>()
        {
            //Settings
            {"EnableWindowedMode", 5}, {"EnablePersistentStereoDesktop", 0}, {"MonitorSize", 48}, {"StereoAdjustEnable", 1}, 
                {"StereoDefaultOn", 1}, {"StereoSeparation", 15},  {"StereoVisionConfirmed", 0},
            {"StereoImageType", 0}, {"SnapShotQuality", 50}, {"LaserSightEnabled", 1}, {"StereoAdvancedHKConfig", 0},
            {"InterleavePattern0", 0x00FF00FF }, {"InterleavePattern1", 0x00FF00FF },
            //KeyBindings
            {"CycleFrustumAdjust", 634}, {"DeleteConfig", 1142}, {"GammaAdjustLess", 1095}, {"GammaAdjustMore", 583},
            { "GlassesDelayMinus",1213}, {"GlassesDelayPlus", 1211}, {"RHWAtScreenLess", 632}, {"RHWAtScreenMore", 633},
            { "RHWLessAtScreenLess", 1144}, {"RHWLessAtScreenMore", 1145}, {"SaveStereoImage", 1136},{"StereoConvergenceAdjustLess", 628},
            { "StereoConvergenceAdjustMore", 629}, {"StereoSeparationAdjustLess", 626}, {"StereoSeparationAdjustMore", 627}, 
            {"StereoSuggestSettings", 625}, {"StereoToggle", 596}, {"StereoToggleMode", 1658}, {"StereoUnsuggestSettings", 1137},
            { "ToggleAutoConvergence", 631}, {"ToggleAutoConvergenceRestore", 1143}, {"ToggleLaserSight", 635}, {"ToggleMemo", 3629},
            { "WriteConfig", 630},
            //Unknown
            {"ControlPanel", 0}, {"DrsEnable", 1}, {"GlassesSwitchDelay", 0}, {"LaserSightIndex", 0}, {"ShowAllViewerTypes", 1},
            { "StereoAnaglyphType", 1}, {"StereoDefaultONSet", 1}, {"StereoDisableTnL", 0}, {"StereoRefreshDefaultOn", 0},
            { "StereoTypeSet", 1}, {"StereoViewerType", 1}, {"Time", 0x560B4EA5}, {"AnaglyphEnabled", 0xDEADBEEF}, {"LaserSightProperty", 0xF004B064},
            { "LeftAnaglyphFilter", 0xFFFF0000}, {"RightAnaglyphFilter", 0xFF00FFFF}, {"StereoGamma", 0x3F800000}, {"StereoMonitorID", 0x27A36904},
            // SPECIAL
            {"InterleavePattern", 0x00FF00FF }
        };

        private static readonly List<string> HotkeyKeys = new List<string>()
        {
            "CycleFrustumAdjust", "DeleteConfig", "GammaAdjustLess", "GammaAdjustMore", "GlassesDelayMinus",
            "GlassesDelayPlus", "RHWAtScreenLess", "RHWAtScreenMore", "RHWLessAtScreenLess", "RHWLessAtScreenMore",
            "SaveStereoImage", "StereoConvergenceAdjustLess",  "StereoConvergenceAdjustMore",  "StereoSeparationAdjustLess", 
            "StereoSeparationAdjustMore", "StereoSuggestSettings", "StereoToggle", "StereoToggleMode", "StereoUnsuggestSettings",
            "ToggleAutoConvergence", "ToggleAutoConvergenceRestore", "ToggleLaserSight", "ToggleMemo", "WriteConfig",
        };

        public static uint GetDefaultKeyValue(string keyName) {
            if (DefaultsDictionary.ContainsKey(keyName))
                return DefaultsDictionary[keyName];
            throw new ArgumentException($"No default is known for: {keyName}", nameof(keyName));
        }

        public static bool KeyIsHotkey(string keyName)
        {
            return HotkeyKeys.Contains(keyName);
        }
    }
}
