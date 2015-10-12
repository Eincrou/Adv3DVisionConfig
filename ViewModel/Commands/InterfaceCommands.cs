namespace Advanced3DVConfig.ViewModel.Commands
{
    using System.Globalization;
    using Advanced3DVConfig.Model;  // Ugly, but gotta do it!
    using System;
    using System.Windows.Input;
    public class ResetASettingCommand : ICommand
    {
        private readonly ViewModel _viewModel;
        public ResetASettingCommand(ViewModel vm)
        {
            _viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            //if (parameter == null) return false;
            if(!(parameter is string)) throw new ArgumentException($"Parameter must be a string representing the name of the Stereo3DKeyName", nameof(parameter));
            object value = _viewModel.GetCurrentKeyValueByString(parameter as string);
            uint inputValue = 0;
            string keyName = (string)parameter;
            if (value is uint)
                inputValue = (uint)value;
            else if (value is bool)
            {
                inputValue = (bool)value ? 1u : 0;
                if (keyName == "EnableWindowedMode" && inputValue == 1) inputValue = 5;
            }
            //else if (value is string)   // Will never happen, but leaving this here in case I ever use string registry keys
            //{
            //    if (!Int32.TryParse((string)value, NumberStyles.HexNumber, null, out inputValue))
            //        return true;
            //}
            uint defaultvalue = Stereo3DRegistryKeyDefaults.GetDefaultKeyValue(keyName);
            return inputValue != defaultvalue;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is string))
                throw new ArgumentException("Command execution requres string with name of registry key",
                    nameof(parameter));
            _viewModel.ResetASetting((string)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class OpenImagesDirectoryCommand : ICommand
    {
        private readonly ViewModel _viewModel;
        public OpenImagesDirectoryCommand(ViewModel vm)
        {
            _viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string stereoImagesDir =
                System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "NVStereoscopic3D.IMG");
            System.Diagnostics.Process.Start(stereoImagesDir);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class ToggleSystemRegistryLockCommand : ICommand
    {
        private readonly ViewModel _viewModel;
        public ToggleSystemRegistryLockCommand(ViewModel vm)
        {
            _viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ToggleSystemRegistryLock();
        }

        public event EventHandler CanExecuteChanged;
    }
}
