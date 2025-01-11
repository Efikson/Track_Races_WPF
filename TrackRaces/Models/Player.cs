using System.Windows;
using System.Windows.Media;

namespace TrackRaces.Models
{
    internal class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Score { get; set; }
        public bool JumpCollected { get; set; }
        public Point Position { get; set; }
        public double Angle { get; set; }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            Score = 0;
        }
    }

}
