using Microsoft.Extensions.DependencyInjection;
using System.Windows.Media;
using TrackRaces.Models;
using TrackRaces.Views;

namespace TrackRaces.ViewModels
{
    public class MainMenuViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public GameSettings GameSettings { get; private set; }

        public MainMenuViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            Player1 = new Player("Player One", Colors.Red);
            Player2 = new Player("Player Two", Colors.Blue);
            GameSettings = new GameSettings();
        }

        public void StartGameWindow()
        {            
            var gameWindowViewModel = _serviceProvider.GetRequiredService<GameWindowViewModel>();
            gameWindowViewModel.SetPlayers(Player1, Player2);
            gameWindowViewModel.SetGameSettings(GameSettings);

            var gameWindow = _serviceProvider.GetRequiredService<GameWindow>();
            gameWindow.DataContext = gameWindowViewModel;

            gameWindowViewModel.StartCountdownTimer();
            gameWindowViewModel.StartBonusTimer();

            gameWindow.Show();
        }   
    }
}