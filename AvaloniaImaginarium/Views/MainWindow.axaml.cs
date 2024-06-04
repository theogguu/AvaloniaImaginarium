using Avalonia.Controls;

namespace AvaloniaImaginarium.Views
{
    public partial class MainWindow : Window
    {
        private static MainWindow? _instance;
        public MainWindow()
        {
            InitializeComponent();
            _instance = this;
        }

        public static MainWindow Instance => _instance ?? new MainWindow();
    }

}