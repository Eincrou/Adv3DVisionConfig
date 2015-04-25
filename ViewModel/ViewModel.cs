using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Advanced3DVConfig.Annotations;
using Advanced3DVConfig.Model;

namespace Advanced3DVConfig.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Stereo3DKeys _s3DKeys;

        #region General
        public int EnablePersistentStereoDesktop {
            get { return _s3DKeys.Stereo3DSettings["EnablePersistentStereoDesktop"]; }
            set { _s3DKeys.Stereo3DSettings["EnablePersistentStereoDesktop"] = value; }
        }
        public bool EnableWindowedMode {
            get { return _s3DKeys.Stereo3DSettings["EnableWindowedMode"] > 0; }
            set
            {
                if (value == true)
                _s3DKeys.Stereo3DSettings["EnableWindowedMode"] =  5;
                else
                {
                    EnablePersistentStereoDesktop = 0;
                    _s3DKeys.Stereo3DSettings["EnableWindowedMode"] = 0;
                }
            }
        }
        public int MonitorSize {
            get { return _s3DKeys.Stereo3DSettings["MonitorSize"]; }
            set { _s3DKeys.Stereo3DSettings["MonitorSize"] = value; }
        }
        public int StereoAdjustEnable {
            get { return _s3DKeys.Stereo3DSettings["StereoAdjustEnable"]; }
            set { _s3DKeys.Stereo3DSettings["StereoAdjustEnable"] = value; }
        }
        public int StereoDefaultOn {
            get { return _s3DKeys.Stereo3DSettings["StereoDefaultOn"]; }
            set { _s3DKeys.Stereo3DSettings["StereoDefaultOn"] = value; }
        }
        public int StereoSeparation{
            get { return _s3DKeys.Stereo3DSettings["StereoSeparation"]; }
            set { _s3DKeys.Stereo3DSettings["StereoSeparation"] = value; }
        }
        public int StereoVisionConfirmed {
            get { return _s3DKeys.Stereo3DSettings["StereoVisionConfirmed"]; }
            set { _s3DKeys.Stereo3DSettings["StereoVisionConfirmed"] = value; }
        }
        #endregion


        #region Screenshots
        public int SaveStereoImage{
            get { return _s3DKeys.Stereo3DHotkeySettings["SaveStereoImage"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["SaveStereoImage"] = value; }
        }
        public int StereoImageType{
            get { return _s3DKeys.Stereo3DSettings["StereoImageType"]; }
            set { _s3DKeys.Stereo3DSettings["StereoImageType"] = value; }
        }
        public int SnapShotQuality{
            get { return _s3DKeys.Stereo3DSettings["SnapShotQuality"]; }
            set { _s3DKeys.Stereo3DSettings["SnapShotQuality"] = value; }
        }
        #endregion
        public int LaserSightEnabled {
            get { return _s3DKeys.Stereo3DSettings["LaserSightEnabled"]; }
            set { _s3DKeys.Stereo3DSettings["LaserSightEnabled"] = value; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
