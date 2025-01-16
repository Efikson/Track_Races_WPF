using System.Windows;
using System.Windows.Input;
using TrackRaces.ViewModels;
using TrackRaces.Models;
using TrackRaces.Views;


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
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {  
            // Direct access to ViewModel via DataContext
            var mainMenuViewModel = (MainMenuViewModel)DataContext;

            var gameWindowViewModel = new GameWindowViewModel(
              mainMenuViewModel.Player1,
              mainMenuViewModel.Player2,
              mainMenuViewModel.GameSettings
            );

            var gameWindow = new GameWindow(gameWindowViewModel);

            gameWindow.Show();
            //Closing main menu
            this.Close();
        }
    }
}
