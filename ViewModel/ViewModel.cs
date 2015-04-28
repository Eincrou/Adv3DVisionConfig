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
        public int StereoSeparation{
            get { return _s3DKeys.Stereo3DSettings["StereoSeparation"]; }
            set { _s3DKeys.Stereo3DSettings["StereoSeparation"] = value; }
        }
        public int StereoToggle {
            get { return _s3DKeys.Stereo3DHotkeySettings["StereoToggle"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["StereoToggle"] = value; }
        }
        public int StereoSeparationAdjustMore {
            get { return _s3DKeys.Stereo3DHotkeySettings["StereoSeparationAdjustMore"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["StereoSeparationAdjustMore"] = value; }
        }
        public int StereoSeparationAdjustLess {
            get { return _s3DKeys.Stereo3DHotkeySettings["StereoSeparationAdjustLess"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["StereoSeparationAdjustLess"] = value; }
        }
        public int StereoConvergenceAdjustMore {
            get { return _s3DKeys.Stereo3DHotkeySettings["StereoConvergenceAdjustMore"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["StereoConvergenceAdjustMore"] = value; }
        }
        public int StereoConvergenceAdjustLess {
            get { return _s3DKeys.Stereo3DHotkeySettings["StereoConvergenceAdjustLess"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["StereoConvergenceAdjustLess"] = value; }
        }
        public int StereoToggleMode {
            get { return _s3DKeys.Stereo3DHotkeySettings["StereoToggleMode"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["StereoToggleMode"] = value; }
        }

        #endregion

        #region Advanced
        public int CycleFrustumAdjust {
            get { return _s3DKeys.Stereo3DHotkeySettings["CycleFrustumAdjust"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["CycleFrustumAdjust"] = value; }
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
        public int StereoVisionConfirmed {
            get { return _s3DKeys.Stereo3DSettings["StereoVisionConfirmed"]; }
            set { _s3DKeys.Stereo3DSettings["StereoVisionConfirmed"] = value; }
        }
        public int ToggleMemo {
            get { return _s3DKeys.Stereo3DHotkeySettings["ToggleMemo"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["ToggleMemo"] = value; }
        }
        public int WriteConfig {
            get { return _s3DKeys.Stereo3DHotkeySettings["WriteConfig"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["WriteConfig"] = value; }
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

        #region Laser Sight
        public int LaserSightEnabled
        {
            get { return _s3DKeys.Stereo3DSettings["LaserSightEnabled"]; }
            set { _s3DKeys.Stereo3DSettings["LaserSightEnabled"] = value; }
        }
        public int ToggleLaserSight
        {
            get { return _s3DKeys.Stereo3DHotkeySettings["ToggleLaserSight"]; }
            set { _s3DKeys.Stereo3DHotkeySettings["ToggleLaserSight"] = value; }
        }

        #endregion
        

        public ViewModel()
        {
            try{
                _s3DKeys = new Stereo3DKeys();
            }
            catch (Exception exception){
                MessageBox.Show(exception.Message, "Registry Error");
            }
        }

        public void SaveSettings(){
            var duplicateHotkeysMessage = _s3DKeys.CheckForDuplicateHotkeys();
            if (duplicateHotkeysMessage != null)
            {
                MessageBox.Show(duplicateHotkeysMessage, "Duplicate hotkeys detected", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            string savedSettings = _s3DKeys.SaveSettingsToRegistry();
            string saveMessage = "Settings saved.\r\n" + savedSettings;
            MessageBox.Show(saveMessage, "Save success");
        }

        public void ResetASetting(string keyName){
            _s3DKeys.ResetKeyToDefault(keyName);
            OnPropertyChanged(keyName);
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
