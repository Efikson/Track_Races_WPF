using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TrackRaces.Models;
using TrackRaces.Services;
using TrackRaces.Views;

namespace TrackRaces.ViewModels
{
    public class MainMenuViewModel : NotifyBase
    {
        private readonly IServiceProvider _serviceProvider;
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public GameSettings GameSettings { get; private set; }

        // Commands
        public RelayCommand StartGameCommand { get; }
        public RelayCommand ExitCommand { get; }

        public MainMenuViewModel(IServiceProvider serviceProvider)        
        {
            _serviceProvider = serviceProvider;
            
            (Player1, Player2) = Player.CreateDefaultPlayers();

            GameSettings = new GameSettings();

            // Initialize commands
            StartGameCommand = new RelayCommand(StartGameWindow);
            ExitCommand = new RelayCommand(ExitApplication);
        }

        public void StartGameWindow()
        {
            // Create gameWindowViewModel and pass dependencies
            var gameWindowViewModel = _serviceProvider.GetRequiredService<GameWindowViewModel>();
            gameWindowViewModel.SetPlayers(Player1, Player2);
            gameWindowViewModel.SetGameSettings(GameSettings);

            // Create gameWindow
            var gameWindow = _serviceProvider.GetRequiredService<GameWindow>();
            gameWindow.DataContext = gameWindowViewModel;

            // Create gameLoop and pass dependencies
            var gameLoop = _serviceProvider.GetRequiredService<GameLoop>();
            gameLoop.SetPlayers(Player1, Player2);
            gameLoop.SetGameSettings(GameSettings);
            gameLoop.SetCanvas(gameWindow.GameCanvas);
            gameLoop.SetViewModel(gameWindowViewModel);

            var mainMenuWindow = _serviceProvider.GetRequiredService<MainMenu>();
            mainMenuWindow.Hide();    
            gameWindow.Show();

            gameWindowViewModel.StartGameCommand.Execute(null);
        }
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }
    }
}