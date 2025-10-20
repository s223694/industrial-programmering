using Avalonia.Controls;
using inventory_assistant.ViewModels;

namespace inventory_assistant.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProcessNextOrder_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
                vm.ProcessNextOrder();
        }
        
    }
}
