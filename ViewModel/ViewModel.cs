using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Advanced3DVConfig.Annotations;
using Advanced3DVConfig.Model;

namespace Advanced3DVConfig.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Stereo3DKeys _s3DKeys;
        private Dictionary<string, Stereo3DRegistryKey> _viewModelRegistryKeys;
        private Dictionary<string, Stereo3DRegistryKey> _previousViewModelRegistryKeys;
        #region General
        public int EnablePersistentStereoDesktop {
            get { return _viewModelRegistryKeys["EnablePersistentStereoDesktop"].KeyValue; }
            set { _viewModelRegistryKeys["EnablePersistentStereoDesktop"].KeyValue = value; }
        }
        public bool EnableWindowedMode {
            get { return _viewModelRegistryKeys["EnableWindowedMode"].KeyValue > 0; }
            set
            {
                if (value)
                    _viewModelRegistryKeys["EnableWindowedMode"].KeyValue = 5;
                else
                {
                    EnablePersistentStereoDesktop = 0;
                    _viewModelRegistryKeys["EnableWindowedMode"].KeyValue = 0;
                }
            }
        }
        public int StereoSeparation{
            get { return _viewModelRegistryKeys["StereoSeparation"].KeyValue; }
            set { _viewModelRegistryKeys["StereoSeparation"].KeyValue = value; }
        }
        public int StereoToggle {
            get { return _viewModelRegistryKeys["StereoToggle"].KeyValue; }
            set { _viewModelRegistryKeys["StereoToggle"].KeyValue = value; }
        }
        public int StereoSeparationAdjustMore {
            get { return _viewModelRegistryKeys["StereoSeparationAdjustMore"].KeyValue; }
            set { _viewModelRegistryKeys["StereoSeparationAdjustMore"].KeyValue = value; }
        }
        public int StereoSeparationAdjustLess {
            get { return _viewModelRegistryKeys["StereoSeparationAdjustLess"].KeyValue; }
            set { _viewModelRegistryKeys["StereoSeparationAdjustLess"].KeyValue = value; }
        }
        public int StereoConvergenceAdjustMore {
            get { return _viewModelRegistryKeys["StereoConvergenceAdjustMore"].KeyValue; }
            set { _viewModelRegistryKeys["StereoConvergenceAdjustMore"].KeyValue = value; }
        }
        public int StereoConvergenceAdjustLess {
            get { return _viewModelRegistryKeys["StereoConvergenceAdjustLess"].KeyValue; }
            set { _viewModelRegistryKeys["StereoConvergenceAdjustLess"].KeyValue = value; }
        }
        public int StereoToggleMode {
            get { return _viewModelRegistryKeys["StereoToggleMode"].KeyValue; }
            set { _viewModelRegistryKeys["StereoToggleMode"].KeyValue = value; }
        }

        #endregion

        #region Advanced
        public int CycleFrustumAdjust {
            get { return _viewModelRegistryKeys["CycleFrustumAdjust"].KeyValue; }
            set { _viewModelRegistryKeys["CycleFrustumAdjust"].KeyValue = value; }
        }
        public int MonitorSize {
            get { return _viewModelRegistryKeys["MonitorSize"].KeyValue; }
            set { _viewModelRegistryKeys["MonitorSize"].KeyValue = value; }
        }
        public int StereoAdjustEnable {
            get { return _viewModelRegistryKeys["StereoAdjustEnable"].KeyValue; }
            set { _viewModelRegistryKeys["StereoAdjustEnable"].KeyValue = value; }
        }
        public int StereoDefaultOn {
            get { return _viewModelRegistryKeys["StereoDefaultOn"].KeyValue; }
            set { _viewModelRegistryKeys["StereoDefaultOn"].KeyValue = value; }
        }
        public int StereoVisionConfirmed {
            get { return _viewModelRegistryKeys["StereoVisionConfirmed"].KeyValue; }
            set { _viewModelRegistryKeys["StereoVisionConfirmed"].KeyValue = value; }
        }
        public int ToggleMemo {
            get { return _viewModelRegistryKeys["ToggleMemo"].KeyValue; }
            set { _viewModelRegistryKeys["ToggleMemo"].KeyValue = value; }
        }
        public int WriteConfig {
            get { return _viewModelRegistryKeys["WriteConfig"].KeyValue; }
            set { _viewModelRegistryKeys["WriteConfig"].KeyValue = value; }
        }
        #endregion

        #region Screenshots
        public int SaveStereoImage{
            get { return _viewModelRegistryKeys["SaveStereoImage"].KeyValue; }
            set { _viewModelRegistryKeys["SaveStereoImage"].KeyValue = value; }
        }
        public int StereoImageType{
            get { return _viewModelRegistryKeys["StereoImageType"].KeyValue; }
            set { _viewModelRegistryKeys["StereoImageType"].KeyValue = value; }
        }
        public int SnapShotQuality{
            get { return _viewModelRegistryKeys["SnapShotQuality"].KeyValue; }
            set { _viewModelRegistryKeys["SnapShotQuality"].KeyValue = value; }
        }
        #endregion

        #region Laser Sight
        public int LaserSightEnabled
        {
            get { return _viewModelRegistryKeys["LaserSightEnabled"].KeyValue; }
            set { _viewModelRegistryKeys["LaserSightEnabled"].KeyValue = value; }
        }
        public int ToggleLaserSight
        {
            get { return _viewModelRegistryKeys["ToggleLaserSight"].KeyValue; }
            set { _viewModelRegistryKeys["ToggleLaserSight"].KeyValue = value; }
        }

        #endregion
        

        public ViewModel()
        {
            try{
                _s3DKeys = new Stereo3DKeys();
                _viewModelRegistryKeys = new Dictionary<string, Stereo3DRegistryKey>();
                var keysList = _s3DKeys.Stereo3DSettings;
                foreach (var key in keysList)
                    _viewModelRegistryKeys.Add(key.KeyName, new Stereo3DRegistryKey(key.KeyName, key.KeyValue));
                _previousViewModelRegistryKeys = new Dictionary<string, Stereo3DRegistryKey>(_viewModelRegistryKeys);
            }
            catch (Exception exception){
                MessageBox.Show(exception.Message, "Registry Error");
            }
        }

        public void SaveSettings(){
            var settingsToList = _viewModelRegistryKeys.Values.ToList();
            var duplicateHotkeysMessage = _s3DKeys.CheckForDuplicateHotkeys(settingsToList);
            if (duplicateHotkeysMessage != null)
            {
                MessageBox.Show(duplicateHotkeysMessage, "Duplicate hotkeys detected", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            string savedSettings = _s3DKeys.SaveSettingsToRegistry(settingsToList);
            string saveMessage = "Settings saved.\r\n" + savedSettings;
            _previousViewModelRegistryKeys = new Dictionary<string, Stereo3DRegistryKey>(_viewModelRegistryKeys);
            MessageBox.Show(saveMessage, "Save success");
        }

        public void ResetASetting(string keyName){
            _viewModelRegistryKeys[keyName].ResetToDefaultValue();
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
