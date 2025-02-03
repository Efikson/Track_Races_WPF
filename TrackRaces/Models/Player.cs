using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using static System.Formats.Asn1.AsnWriter;

namespace TrackRaces.Models
{
    public class Player : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public Color Color { get; set; }        
        public Point Position { get; set; }
        public double Angle { get; set; }

        private int score;
        public int Score
        {
            get => score;
            set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool jumpCollected;
        public bool JumpCollected
        {
            get => jumpCollected;
            set
            {
                if (jumpCollected != value)
                {
                    jumpCollected = value;
                    OnPropertyChanged();
                }
            }
        }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
