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

        public void UpdatePlayerMovements()
        {            
            if (Keyboard.IsKeyDown(Key.A))
            {
                RotatePlayer(Player1, -TurnSpeed);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                RotatePlayer(Player1, TurnSpeed);
            }
   
            if (Keyboard.IsKeyDown(Key.Left))
            {
                RotatePlayer(Player2, -TurnSpeed);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                RotatePlayer(Player2, TurnSpeed);
            }
        }

        private void RotatePlayer(Player player, double angleChange)
        {
            player.Angle += angleChange;
        }

    }
}
