namespace Advanced3DVConfig.View
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows;
    using ViewModel;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string KeyComboUrl =
            "http://3dvision-blog.com/3053-modifying-all-3d-vision-control-key-combinations-as-you-need/";

        public MainWindow() {
            InitializeComponent();
        }

        private void ModifiersRefBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(KeyComboUrl);
        }
    }
}
