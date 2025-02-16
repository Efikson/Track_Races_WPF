using System.Windows;
using System.Windows.Input;
using TrackRaces.ViewModels;
using TrackRaces.Models;
using TrackRaces.Views;
using Microsoft.Extensions.DependencyInjection;
using TrackRaces.Logic;


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
                Application.Current.Shutdown();
            }
        }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {         
            // Get access to the ViewModel
            var mainMenuViewModel = (MainMenuViewModel)DataContext;           
            mainMenuViewModel.StartGameWindow();

            // Hide main menu
            this.Hide();
        }
    }
}
