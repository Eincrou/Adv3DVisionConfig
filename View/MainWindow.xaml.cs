
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Advanced3DVConfig.View
{
    using System.Windows;
    using ViewModel;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel;
        public MainWindow() {
            InitializeComponent();

            _viewModel = FindResource("ViewModel") as ViewModel;

// ReSharper disable once PossibleNullReferenceException
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save these settings to the Windows Registry?", 
                "Confirm Save", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                _viewModel.SaveSettings();
        }

        private void ModifiersRefBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://3dvision-blog.com/3053-modifying-all-3d-vision-control-key-combinations-as-you-need/");
        }
    }
}
