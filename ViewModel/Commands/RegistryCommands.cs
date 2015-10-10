namespace Advanced3DVConfig.ViewModel.Commands
{
    using System;
    using System.Windows.Input;
    public class SaveSettingsToRegistryCommand : ICommand
    {
        private readonly ViewModel _viewModel;
        public SaveSettingsToRegistryCommand(ViewModel vm)
        {
            _viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SaveSettingsToRegistry();
        }

        public event EventHandler CanExecuteChanged;
    }
}
