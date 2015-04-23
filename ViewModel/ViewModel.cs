using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Advanced3DVConfig.Model;

namespace Advanced3DVConfig.ViewModel
{
    public class ViewModel
    {
        private readonly Stereo3DKeys _s3DKeys;

        public int StereoSeparation
        {
            get { return _s3DKeys.Stereo3DSettings["StereoSeparation"]; }
            set { _s3DKeys.Stereo3DSettings["StereoSeparation"] = value; }
        }
        public int MonitorSize {
            get { return _s3DKeys.Stereo3DSettings["MonitorSize"]; }
            set { _s3DKeys.Stereo3DSettings["MonitorSize"] = value; }
        }

        public int SaveStereoImage
        {
            get { return _s3DKeys.Stereo3DSettings["SaveStereoImage"]; }
            set { _s3DKeys.Stereo3DSettings["SaveStereoImage"] = value; }
        }
        public int StereoImageType
        {
            get { return _s3DKeys.Stereo3DSettings["StereoImageType"]; }
            set { _s3DKeys.Stereo3DSettings["StereoImageType"] = value; }
        }
        public int SnapShotQuality { 
            get { return _s3DKeys.Stereo3DSettings["SnapShotQuality"]; }
            set { _s3DKeys.Stereo3DSettings["SnapShotQuality"] = value; }
        }

        public ViewModel()
        {
            try
            {
                _s3DKeys = new Stereo3DKeys();
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "Registry Error");
            }
        }

        public void SaveSettings()
        {
            string savedSettings = _s3DKeys.SaveSettingsToRegistry();
            string saveMessage = "Settings saved.\r\n" + savedSettings;
            MessageBox.Show(saveMessage, "Save success");
        }
    }
}
