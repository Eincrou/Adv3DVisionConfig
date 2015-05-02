using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced3DVConfig.Model
{
    public class Stereo3DRegistryKeyDefaults
    {
        private static Dictionary<string, int> defaultsDictionary = new Dictionary<string, int>()
        {
            //Settings
            {"EnableWindowedMode", 5}, {"EnablePersistentStereoDesktop", 0}, {"MonitorSize", -1}, {"StereoAdjustEnable", 1}, 
                {"StereoDefaultOn", 1}, {"StereoSeparation", 15},  {"StereoVisionConfirmed", 0},
            {"StereoImageType", 0}, {"SnapShotQuality", 50},
            {"LaserSightEnabled", 1},
            //KeyBindings
            {"CycleFrustumAdjust", 634}, {"DeleteConfig", 1142}, {"GammaAdjustLess", 1095}, {"GammaAdjustMore", 583}, {"GlassesDelayMinus",1213}, {"GlassesDelayPlus", 1211}, 
            {"RHWAtScreenLess", 632}, {"RHWAtScreenMore", 633}, {"RHWLessAtScreenLess", 1144}, {"RHWLessAtScreenMore", 1145}, {"SaveStereoImage", 1136},
            {"StereoConvergenceAdjustLess", 628}, {"StereoConvergenceAdjustMore", 629}, {"StereoSeparationAdjustLess", 626}, {"StereoSeparationAdjustMore", 627}, 
            {"StereoSuggestSettings", 625}, {"StereoToggle", 596}, {"StereoToggleMode", 1658}, {"StereoUnsuggestSettings", 1137}, {"ToggleAutoConvergence", 631}, 
            {"ToggleAutoConvergenceRestore", 1143}, {"ToggleLaserSight", 635}, {"ToggleMemo", 3629}, {"WriteConfig", 630},
        };

        public static int GetDefaultKeyValue(string keyName) {
            if (defaultsDictionary.ContainsKey(keyName))
                return defaultsDictionary[keyName];
            throw new ArgumentException("No default value is known for this keyValue.", "keyName");
        }
    }
}
