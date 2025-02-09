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
        private GameWindowViewModel _viewModel;
        private DispatcherTimer countdownTimer;
        private DispatcherTimer bonusTimer;
        private Canvas GameCanvas;
        private Random random = new Random();
        private Ellipse bonusShape;
        public Ellipse BonusShape
        {
            get => bonusShape;
            private set => bonusShape = value;
        }

        private string countdownValue;
        public string CountdownValue
        {
            get => countdownValue;
            set
            {
                if (countdownValue != value)
                {
                    countdownValue = value;
                    OnPropertyChanged(nameof(CountdownValue));
                }
            }
        }

        private int timeUntilBonus;
        public int TimeUntilBonus
        {
            get => timeUntilBonus;
            set
            {
                if (timeUntilBonus != value)
                {
                    timeUntilBonus = value;
                    OnPropertyChanged(nameof(timeUntilBonus));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
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
            CountdownValue = "2";// Value 2 for testing purposes
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

        public void RemoveBonus()
        {           
            GameCanvas.Children.Remove(bonusShape);
            bonusShape = null;            
        }

        public void StopTimers()
        {
            countdownTimer.Stop();
            bonusTimer.Stop();
        }
    }
}
