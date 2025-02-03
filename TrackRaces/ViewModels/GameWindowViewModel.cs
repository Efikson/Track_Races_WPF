using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TrackRaces.Logic;
using TrackRaces.Models;
using TrackRaces.Views;

namespace TrackRaces.ViewModels
{
    public class GameWindowViewModel
    {
        // Public properties for data binding
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public GameSettings GameSettings { get; set; }
        public int TimeUntilBonus { get; set; }
        private Canvas GameCanvas;

        // Declaring dependencies as private fields
        private readonly GameRenderer _gameRenderer;
        private readonly PlayerController _playerController;
        private readonly PlayerCollision _playerCollision;
        private readonly TimerManager _timerManager;

        // "forwarding" public property for UI binding
        public TimerManager TimerManager => _timerManager;

        private DispatcherTimer gameTickTimer;     
        public GameWindowViewModel(GameRenderer gameRenderer,
                                   PlayerController playerController,
                                   PlayerCollision playerCollision,
                                   TimerManager timerManager)
        {                        
            _gameRenderer = gameRenderer;
            _playerController = playerController;
            _playerCollision = playerCollision;
            _timerManager = timerManager;           
        }

        public void SetPlayers(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            _gameRenderer.SetPlayers(player1, player2);
            _playerController.SetPlayers(player1, player2);
            _playerCollision.SetPlayers(player1, player2);
        }

        public void SetGameSettings(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
            _gameRenderer.SetGameSettings(gameSettings);
            _playerCollision.SetGameSettings(gameSettings);
        }

        public void SetCanvas(Canvas canvas)
        {
            GameCanvas = canvas;
            _gameRenderer.SetCanvas(canvas);
            _timerManager.SetCanvas(canvas);
            _playerCollision.SetCanvas(canvas);
        }
        public void SetViewModel(GameWindowViewModel viewModel)
        {
            _timerManager.SetViewModel(this);
            _playerCollision.SetViewModel(this);
        }

        public void StartGameTickTimer()
        {
            gameTickTimer = new DispatcherTimer();
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(16.66); // 33.33 - 30 FPS || 16.66 - 60 FPS
            gameTickTimer.Tick += GameTickTimer_Tick;
            gameTickTimer.Start();
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            _playerCollision.CheckPlayerCollision(1);
            _playerCollision.CheckPlayerCollision(2);
            _playerController.UpdatePlayerMovements();
            _gameRenderer.UpdatePlayerPositions(Player1, Player2);
        }
        
        public void StartGame()
        {
            _gameRenderer.ResetPlayerPosition();
            StartGameTickTimer();
        }

        public void ReturnToMainMenu()
        {
            var mainMenu = App.ServiceProvider.GetRequiredService<MainMenu>();
            mainMenu.Show();

            Application.Current.Windows
                .OfType<GameWindow>()
                .FirstOrDefault()?.Hide();
        }

        public void StartCountdownTimer()
        {
            _timerManager.StartCountdown();
        }

        public void StartBonusTimer()
        {
            _timerManager.StartBonusTimer();
        }
        
        public void StopAllTimers()
        {
            gameTickTimer.Stop();
            _timerManager.StopTimers();
        }
    }
}
