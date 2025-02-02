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
        private readonly Canvas gameCanvas;
        private readonly Random random = new Random();
        private Ellipse bonusShape;
        private readonly GameWindowViewModel viewModel;

        private string countdownValue;
        public string CountdownValue
        {
            get => countdownValue;
            private set { countdownValue = value; NotifyPropertyChanged(); }
        }

        private int timeUntilBonus;
        public int TimeUntilBonus
        {
            get => timeUntilBonus;
            private set { timeUntilBonus = value; NotifyPropertyChanged(); }
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public TimerManager(GameWindowViewModel viewModel,Canvas canvas)
        {
            this.viewModel = viewModel;
            gameCanvas = canvas;
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

                viewModel.StartGame();
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
                gameCanvas.Children.Add(bonusShape);
            }

            double x = random.Next(10, (int)(gameCanvas.ActualWidth - bonusShape.Width - 10));
            double y = random.Next(10, (int)(gameCanvas.ActualHeight - bonusShape.Height - 10));

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
