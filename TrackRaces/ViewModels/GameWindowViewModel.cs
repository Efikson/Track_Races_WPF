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
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public GameSettings GameSettings { get; private set; }

        // Other components
        public GameRenderer GameRenderer;
        public PlayerController PlayerController;
        public PlayerCollision PlayerCollision;
        public TimerManager TimerManager;
       
        private Canvas gameCanvas;
        private DispatcherTimer gameTickTimer;

        public GameWindowViewModel()
        {
            
        }

        public void SetPlayers(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void SetGameSettings(GameSettings settings)
        {
            GameSettings = settings;
        }

        public void SetCanvas(Canvas canvas)
        {
            gameCanvas = canvas;           
        }

        public void SetGameRenderer()
        {
            GameRenderer = new GameRenderer(gameCanvas, GameSettings, Player1, Player2);
        }

        public void SetPlayerController()
        {
            PlayerController = new PlayerController(Player1, Player2);
        }

        public void SetPlayerCollision()
        {
            PlayerCollision = new PlayerCollision(this, gameCanvas, GameSettings, Player1, Player2);
        }
        public void SetTimerManager()
        {
            TimerManager = new TimerManager(this, gameCanvas);
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
            PlayerCollision.CheckPlayerCollision(1);
            PlayerCollision.CheckPlayerCollision(2);
            PlayerController.UpdatePlayerMovements();
            GameRenderer.UpdatePlayerPositions(Player1, Player2);
        }

        // Metoda, která se spustí po dokončení odpočtu (z BonusManageru)
        public void StartGame()
        {
            GameRenderer.ResetPlayerPosition();
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
            TimerManager.StartCountdown();
        }

        public void StartBonusTimer()
        {
            TimerManager.StartBonusTimer();
        }
        
        public void StopAllTimers()
        {
            gameTickTimer.Stop();
            TimerManager.StopTimers();            
        }
    }
}
