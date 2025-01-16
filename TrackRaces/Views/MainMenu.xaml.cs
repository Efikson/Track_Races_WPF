using System.Windows;
using System.Windows.Input;
using TrackRaces.ViewModels;

namespace TrackRaces.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            InitializeComponent();

            // Set DataContext for binding
            DataContext = mainMenuViewModel;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.Key == Key.Escape)
            {
                this.Close(); 
            }
        }
    }
}
