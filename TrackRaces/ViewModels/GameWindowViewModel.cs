using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrackRaces.Logic;
using TrackRaces.Models;
using TrackRaces.Views;

namespace TrackRaces.ViewModels
{
    public class GameWindowViewModel : INotifyPropertyChanged
    {
        public Player Player1 { get; private set; } 
        public Player Player2 { get; private set; } 
        public GameSettings GameSettings { get; private set; }

        private DispatcherTimer countdownTimer;

        private string countdownValue;

        private Canvas gameCanvas;
        private GameRenderer gameRenderer;
        private PlayerController playerController;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GameWindowViewModel()
        {

        }

        public void SetPlayers(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void SetGameSettings(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
        }

        public void SetCanvas(Canvas canvas)
        {
            gameCanvas = canvas;
        }

        public void SetGameRenderer()
        {               
            gameRenderer = new GameRenderer(gameCanvas, GameSettings, Player1 , Player2);
        }

        public void SetPlayerController()
        {
            playerController = new PlayerController(Player1, Player2);
        }

        private void StartGame()
        {
            gameRenderer.ResetPlayerPosition();
            // ResetCanvas(gameCanvas)
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

        public string CountdownValue
        {
            get { return countdownValue; }
            set
            {
                if (countdownValue != value)
                {
                    countdownValue = value;
                    NotifyPropertyChanged(nameof(CountdownValue));
                }
            }
        }

        public void StartCountdown()
        {
            CountdownValue = "1"; // Value 1 for testing purposes
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            int currentTimerValue = int.Parse(CountdownValue);
            currentTimerValue--;

            if (currentTimerValue > 0)
            {
                CountdownValue = currentTimerValue.ToString();
            }
            else
            {
                countdownTimer.Stop();
                CountdownValue = "";
                
                StartGame();
            }
        }

        private void StartGameTickTimer()
        {
            DispatcherTimer gameTickTimer = new DispatcherTimer();
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(16.66); // 33.33 - 30 FPS || 16.66 - 60 FPS
            gameTickTimer.Tick += GameTickTimer_Tick;
            gameTickTimer.Start();
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            playerController.UpdatePlayerMovements();
            gameRenderer.UpdatePlayerPositions(Player1, Player2);            
        }
    }
}
