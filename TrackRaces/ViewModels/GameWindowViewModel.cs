using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrackRaces.Models;

namespace TrackRaces.ViewModels
{
    public class GameWindowViewModel : INotifyPropertyChanged
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        public GameSettings GameSettings { get; }

        private DispatcherTimer countdownTimer;

        private string countdownValue;

        private Canvas gameCanvas;

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

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameWindowViewModel(Player player1, Player player2, GameSettings gameSettings)
        {
            Player1 = player1;
            Player2 = player2;
            GameSettings = gameSettings;            
        }

        public void StartCountdown()
        {
            CountdownValue = "3";
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

        private void StartGame()
        {
            ResetPlayerPosition(gameCanvas);
            // ResetCanvas(gameCanvas)
            StartGameTickTimer();                                   
        }

        private void ResetPlayerPosition(Canvas gameCanvas)
        {
            Random random = new Random();
            double canvasWidth = gameCanvas.ActualWidth;
            double canvasHeight = gameCanvas.ActualHeight;
           
            Player1.Position = new Point(canvasWidth * 0.25, canvasHeight * 0.5);
            Player2.Position = new Point(canvasWidth * 0.75, canvasHeight * 0.5);

            Player1.Angle = random.Next(0, 360); 
            Player2.Angle = random.Next(0, 360);
        }

        private void StartGameTickTimer()
        {
            DispatcherTimer gameTickTimer = new DispatcherTimer();
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(33.33); // 30 FPS
            gameTickTimer.Tick += GameTickTimer_Tick;
            gameTickTimer.Start();
        }
        private void GameTickTimer_Tick(object sender, EventArgs e)
        {            
            UpdatePlayerPositions(gameCanvas);
            //CheckCollisions(gameCanvas);            
        }

        public void SetCanvas(Canvas canvas)
        {
            gameCanvas = canvas;
        }

        public void UpdatePlayerPositions(Canvas gameCanvas)
        {            
            MovePlayer(Player1, gameCanvas);
            MovePlayer(Player2, gameCanvas);
        }        

        private void MovePlayer(Player player, Canvas gameCanvas)
        {            
            double radians = player.Angle * (Math.PI / 180); // Convert degrees to radians
            double newX = player.Position.X + GameSettings.LineSpeed * Math.Cos(radians);
            double newY = player.Position.Y + GameSettings.LineSpeed * Math.Sin(radians);
           
            DrawLine(gameCanvas, player.Position, new Point(newX, newY), player.Color);
            
            player.Position = new Point(newX, newY);
        }

        private void DrawLine(Canvas canvas, Point start, Point end, Color color)
        {
            // Convert Color to SolidColorBrush
            Brush brushColor = new SolidColorBrush(color);
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = brushColor,
                StrokeThickness = GameSettings.LineThickness
            };
            canvas.Children.Add(line);
        }

    }
}
