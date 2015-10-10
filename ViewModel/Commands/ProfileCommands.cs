namespace Advanced3DVConfig.ViewModel.Commands
{
    using System;
    using System.Windows.Input;
    public class SaveProfileCommand : ICommand
    {
        private readonly ViewModel _viewModel;
        public SaveProfileCommand(ViewModel vm)
        {
            _viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SaveSettingsProfile();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class LoadProfileCommand : ICommand
    {
        private readonly ViewModel _viewModel;
        public LoadProfileCommand(ViewModel vm)
        {
            _viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.LoadSettingsProfile();
        }

        public event EventHandler CanExecuteChanged;
    }
}
