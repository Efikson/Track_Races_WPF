using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrackRaces.Services;
using TrackRaces.ViewModels;

namespace TrackRaces.Models
{
    public class TimerManager : NotifyBase
    {
        private GameWindowViewModel _viewModel;
        private Canvas GameCanvas;
        private DispatcherTimer countdownTimer;
        private DispatcherTimer bonusTimer;
        private readonly GameRenderer _gameRenderer;

        public event Action CountdownFinished;

        private string _countdownValue;
        public string CountdownValue
        {
            get => _countdownValue;
            set
            {
                if (_countdownValue != value)
                {
                    _countdownValue = value;
                    OnPropertyChanged(nameof(CountdownValue));
                }
            }
        }

        private int _timeUntilBonus;
        public int TimeUntilBonus
        {
            get => _timeUntilBonus;
            set
            {
                if (_timeUntilBonus != value)
                {
                    _timeUntilBonus = value;
                    OnPropertyChanged(nameof(TimeUntilBonus));
                }
            }
        }

        public TimerManager(GameRenderer gameRenderer)
        {
            _gameRenderer = gameRenderer;
        }

        public void SetCanvas(Canvas canvas)
        {
            GameCanvas = canvas;
        }

        public void SetViewModel(GameWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void StartTimers()
        {
           StartCountdownTimer();
           StartBonusTimer();
        }

        public void StartCountdownTimer()
        {
            CountdownValue = "3";
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

                CountdownFinished.Invoke();
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
                _gameRenderer.SpawnBonus();
            }
        }

        public void StopTimers()
        {
            countdownTimer.Stop();
            bonusTimer.Stop();
        }
    }
}
