using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrackRaces.Models;
using TrackRaces.ViewModels;

namespace TrackRaces.Logic
{    
    public class TimerManager : INotifyPropertyChanged
    {
        private DispatcherTimer countdownTimer;
        private DispatcherTimer bonusTimer;
        private Canvas GameCanvas;
        private Random random = new Random();
        private Ellipse bonusShape;
        private GameWindowViewModel _viewModel;

        private string countdownValue;
        public string CountdownValue
        {
            get => countdownValue;
            private set { countdownValue = value; OnPropertyChanged(); }
        }

        private int timeUntilBonus;
        public int TimeUntilBonus
        {
            get => timeUntilBonus;
            private set { timeUntilBonus = value; OnPropertyChanged(); }
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public TimerManager()
        {
            
        }
        public void SetCanvas(Canvas canvas)
        {
            GameCanvas = canvas;
        }
        public void SetViewModel(GameWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void StartCountdown()
        {
            CountdownValue = "1";// Value 1 for testing purposes
            countdownTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            int current = int.Parse(CountdownValue);
            current--;

            if (current > 0)
            {
                CountdownValue = current.ToString();
            }
            else
            {
                countdownTimer.Stop();
                CountdownValue = "";

                _viewModel.StartGame();
            }
        }
        
        public void StartBonusTimer()
        {
            TimeUntilBonus = 5;
            bonusTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
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
                bonusTimer.Stop();
                TimeUntilBonus = 5;
                SpawnBonus();
            }
        }
        
        public void SpawnBonus()
        {
            if (bonusShape == null)
            {
                bonusShape = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Gold
                };
                GameCanvas.Children.Add(bonusShape);
            }

            double x = random.Next(10, (int)(GameCanvas.ActualWidth - bonusShape.Width - 10));
            double y = random.Next(10, (int)(GameCanvas.ActualHeight - bonusShape.Height - 10));

            Canvas.SetLeft(bonusShape, x);
            Canvas.SetTop(bonusShape, y);
        }
        
        public void StopTimers()
        {
            countdownTimer.Stop();
            bonusTimer.Stop();
        }
    }
}
