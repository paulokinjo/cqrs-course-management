using System.Windows;

namespace Client.Common
{
    /// <summary>
    /// Interaction logic for CustomWindow.xaml
    /// </summary>
    public partial class CustomWindow
    {
        public CustomWindow(ViewModel viewModel)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            DataContext = viewModel;
            Width = viewModel.Width;
            Height = viewModel.Height;
        }
    }
}
