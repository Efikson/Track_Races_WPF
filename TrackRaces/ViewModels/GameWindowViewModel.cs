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

        private DispatcherTimer gameTickTimer;
        private DispatcherTimer countdownTimer;
        private DispatcherTimer bonusTimer;

        private string countdownValue;
        private int timeUntilBonus;

        private Canvas gameCanvas;
        private GameRenderer gameRenderer;
        private PlayerController playerController;
        private PlayerCollision playerCollision;
        private Random random = new Random();
        private Ellipse bonusShape;

        public string CountdownValue
        {
            get { return countdownValue; }
            set
            {
                countdownValue = value;
                NotifyPropertyChanged(nameof(CountdownValue));
            }
        }
        public int TimeUntilBonus
        {
            get { return timeUntilBonus; }
            set
            {
                timeUntilBonus = value;
                NotifyPropertyChanged(nameof(TimeUntilBonus));
            }
        }

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
        public void SetPlayerCollision()
        {
            playerCollision = new PlayerCollision(this, gameCanvas, GameSettings, Player1, Player2);
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
            gameTickTimer = new DispatcherTimer();
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(16.66); // 33.33 - 30 FPS || 16.66 - 60 FPS
            gameTickTimer.Tick += GameTickTimer_Tick;
            gameTickTimer.Start();
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            playerCollision.CheckPlayerCollision(1);
            playerCollision.CheckPlayerCollision(2);
            playerController.UpdatePlayerMovements();
            gameRenderer.UpdatePlayerPositions(Player1, Player2);

        }

        public void StartBonusTimer()
        {
            TimeUntilBonus = 5;
            bonusTimer = new DispatcherTimer();
            bonusTimer.Interval = TimeSpan.FromSeconds(1);
            bonusTimer.Tick += BonusTimer_Tick;
            bonusTimer.Start();
        }

        private void BonusTimer_Tick(object sender, EventArgs e)
        {
            if (TimeUntilBonus > 1)
            {
                TimeUntilBonus--;
            }
            else
            {
                TimeUntilBonus = 5;
                SpawnBonus();               
            }
        }

        private void SpawnBonus()
        {            
            if (bonusShape == null)
            {
                bonusShape = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Gold
                };
               
                gameCanvas.Children.Add(bonusShape);
            }
            
            double x = random.Next(10, (int)(gameCanvas.ActualWidth - bonusShape.Width - 10)); 
            double y = random.Next(10, (int)(gameCanvas.ActualHeight - bonusShape.Height - 10)); 
           
            Canvas.SetLeft(bonusShape, x);
            Canvas.SetTop(bonusShape, y);
        }

        public void StopAllTimers()
        {
            gameTickTimer.Stop();
            bonusTimer.Stop();           
        }
    }
}
