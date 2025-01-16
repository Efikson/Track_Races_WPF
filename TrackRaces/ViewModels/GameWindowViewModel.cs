using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

                // Method to start the game
            }
        }
    }
}
