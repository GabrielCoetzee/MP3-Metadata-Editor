using MP3_MetadataEditor_Client.MVVM.ViewModels;
using System;
using System.Windows;

namespace MP3_MetadataEditor_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            DataContext = new ViewModelMp3();
        }
    }
}
