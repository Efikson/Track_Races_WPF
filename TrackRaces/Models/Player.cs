using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using TrackRaces.Services;
using static System.Formats.Asn1.AsnWriter;

namespace TrackRaces.Models
{
    public class Player : NotifyBase
    {
        public string Name { get; set; }
        public Color Color { get; set; }        
        public Point Position { get; set; }
        public double Angle { get; set; }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _jumpCollected;
        public bool JumpCollected
        {
            get => _jumpCollected;
            set
            {
                if (_jumpCollected != value)
                {
                    _jumpCollected = value;
                    OnPropertyChanged();
                }
            }
        }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public static (Player, Player) CreateDefaultPlayers()
        {
            var player1 = new Player("Player One", Colors.Red);
            var player2 = new Player("Player Two", Colors.Blue);
            return (player1, player2);
        }
    }
}
