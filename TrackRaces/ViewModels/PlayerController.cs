using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TrackRaces.Models;
using TrackRaces.ViewModels;
using TrackRaces.Views;

namespace TrackRaces.Logic
{
    public class PlayerController
    {
        private readonly Player Player1;
        private readonly Player Player2;       
        public double TurnSpeed { get; set; } = 5.0;

        public PlayerController(Player Player1, Player Player2)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;            
        }

        public void HandleInput(Key key)
        {
            switch (key)
            {
                // Player 1: A, D
                case Key.A:
                    RotatePlayer(Player1, -TurnSpeed);
                    break;
                case Key.D:
                    RotatePlayer(Player1, TurnSpeed);
                    break;

                // Player 2: Left, Right
                case Key.Left:
                    RotatePlayer(Player2, -TurnSpeed);
                    break;
                case Key.Right:
                    RotatePlayer(Player2, TurnSpeed);
                    break;

                default:
                    // No action for other keys
                    break;
            }
        }
        private void RotatePlayer(Player Player, double angleChange)
        {
            Player.Angle += angleChange;
        }

    }
}
