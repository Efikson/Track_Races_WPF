using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using TrackRaces.Models;
using TrackRaces.Services;

namespace TrackRaces.ViewModels
{
    public class GameWindowViewModel : NotifyBase
    {
        private Player _player1;
        private Player _player2;
        private GameSettings _gameSettings;   
        private readonly GameLoop _gameLoop;
        private readonly TimerManager _timerManager;        
        public Player Player1 => _player1;
        public Player Player2 => _player2;
        public GameSettings GameSettings => _gameSettings;
        public TimerManager TimerManager => _timerManager;

        // Commands
        public RelayCommand StartGameCommand { get; }
        public RelayCommand ReturnToMenuCommand { get; }

        public GameWindowViewModel(GameLoop gameLoop, TimerManager timerManager)
        {
            _gameLoop = gameLoop;
            _timerManager = timerManager;

            // Initialize commands
            StartGameCommand = new RelayCommand(StartGame);
            ReturnToMenuCommand = new RelayCommand(ReturnToMenu);
        }   

        public void SetPlayers(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;            
        }

        public void SetGameSettings(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void StartGame()
        {
            _gameLoop.StartGame();
        }

        private void ReturnToMenu()
        {
            _gameLoop.ReturnToMenu();
        }
    }
}
