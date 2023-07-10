using System.Windows;

namespace ValidationResearch
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //DataContext = new MainViewModelBasic();
            DataContext = new MainViewModelAdvanced();
            InitializeComponent();
        }
    }
}