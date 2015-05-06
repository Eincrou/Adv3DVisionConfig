
using System;
using System.Windows.Controls;
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
                _viewModel.SaveSettings();
        }

        private void ModifiersRefBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://3dvision-blog.com/3053-modifying-all-3d-vision-control-key-combinations-as-you-need/");
        }

        private void OpenImagesDirBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string stereoImagesDir =
                System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "NVStereoscopic3D.IMG");
            System.Diagnostics.Process.Start(stereoImagesDir);
        }

        private void ResetSettingButton_Click(object sender, RoutedEventArgs e){
            _viewModel.ResetASetting(((Button)sender).Tag.ToString());
        }

        private void SaveProfileButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveSettingsProfile();
        }

        private void LoadProfileButton_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadSettingsProfile();
        }
    }
}
