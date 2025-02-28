using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TrackRaces.Services;
using TrackRaces.ViewModels;
using TrackRaces.Views;

namespace TrackRaces.Models
{
    public class GameLoop : NotifyBase
    {
        private Player _player1;
        private Player _player2;
        private GameSettings _gameSettings;

        // Declaring dependencies as private fields
        private readonly GameRenderer _gameRenderer;
        private readonly PlayerController _playerController;
        private readonly PlayerCollision _playerCollision;
        private readonly TimerManager _timerManager;

        private Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _lastUpdateTime = TimeSpan.Zero;
        private const double TargetFps = 60.0;
        private const double TimePerFrame = 1000.0 / TargetFps;

        private bool CanStartNewRound = true;

        public GameLoop(GameRenderer gameRenderer,
                        PlayerController playerController,
                        PlayerCollision playerCollision,
                        TimerManager timerManager)
        {
            _gameRenderer = gameRenderer;
            _playerController = playerController;
            _playerCollision = playerCollision;
            _timerManager = timerManager;

            // Listening to events
            _timerManager.CountdownFinished += StartGameLoop;
            _playerCollision.CollisionOccured += StopGameLoop;
            _playerCollision.CollisionOccured += CheckWinCondition;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            _gameRenderer.SetPlayers(player1, player2);
            _playerController.SetPlayers(player1, player2);
            _playerCollision.SetPlayers(player1, player2);
        }

        public void SetGameSettings(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameRenderer.SetGameSettings(gameSettings);
            _playerCollision.SetGameSettings(gameSettings);
        }

        public void SetCanvas(Canvas gameCanvas)
        {
            _gameRenderer.SetCanvas(gameCanvas);
            _playerCollision.SetCanvas(gameCanvas);
            _timerManager.SetCanvas(gameCanvas);
        }

        public void SetViewModel(GameWindowViewModel viewModel)
        {
            _timerManager.SetViewModel(viewModel);
            _playerCollision.SetViewModel(viewModel);
        }

        public void StartGame()
        { 
            if (!CanStartNewRound)
            {
                return;
            }
            CanStartNewRound = false;
            ResetGameLoop();
            _timerManager.StartTimers();
            // After finishing countdown event is raised and StartGameLoop is called
        }

        public void StartGameLoop()
        {            
            CompositionTarget.Rendering += OnRender;
            _stopwatch.Start();
        }

        private void OnRender(object sender, EventArgs e)
        {
            var elapsedTime = _stopwatch.Elapsed;

            // Is it time for a new frame?
            if (elapsedTime - _lastUpdateTime >= TimeSpan.FromMilliseconds(TimePerFrame))
            {
                _lastUpdateTime = elapsedTime;
                
                _playerController.UpdatePlayerMovements();
                _playerCollision.CheckPlayerCollision(_player1);
                _playerCollision.CheckPlayerCollision(_player2);
                _gameRenderer.UpdatePlayerPositions();
            }
        }

        public void ResetGameLoop()
        {
            _gameRenderer.RemovePlayerTracks();
            _gameRenderer.ResetPlayerPosition();
            _gameRenderer.ResetPlayerBonus();            
        }

        public void StopGameLoop()
        {
            CompositionTarget.Rendering -= OnRender;
            _stopwatch.Stop();
            _timerManager.StopTimers();
            CanStartNewRound = true;
        }

        public void CheckWinCondition()
        {
            if (_player1.Score >= _gameSettings.TargetScore)
            {
                MessageBox.Show($"{_player1.Name} wins!");
                ReturnToMenu();
            }
            else if (_player2.Score >= _gameSettings.TargetScore)
            {
                MessageBox.Show($"{_player2.Name} wins!");
                ReturnToMenu();
            }            
        }

        public void ReturnToMenu()
        {
            if (!CanStartNewRound)
            {
                return;
            }

            StopGameLoop();
            _gameRenderer.ResetPlayerScore();
            var mainMenu = App.ServiceProvider.GetRequiredService<MainMenu>();
            mainMenu.Show();

            Application.Current.Windows
                .OfType<GameWindow>()
                .FirstOrDefault()?.Hide();
        }
    }
}
