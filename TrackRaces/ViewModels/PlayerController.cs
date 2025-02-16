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
        private Player Player1;
        private Player Player2;       
        public double TurnSpeed { get; set; } = 5.0;
        private readonly GameRenderer _gameRenderer;
        private readonly PlayerCollision _playerCollision;

        public PlayerController(GameRenderer gameRenderer, PlayerCollision playerCollision)
        {
            _gameRenderer = gameRenderer;
            _playerCollision = playerCollision;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
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
            if (Keyboard.IsKeyDown(Key.Q))
            {
                _gameRenderer.ProcessJump(Player1);
                _playerCollision.CheckJumpCollision(Player1);
            }
            if (Keyboard.IsKeyDown(Key.NumPad0))
            {
                _gameRenderer.ProcessJump(Player2);
                _playerCollision.CheckJumpCollision(Player2);
            }
        }

        private void RotatePlayer(Player player, double angleChange)
        {
            player.Angle += angleChange;
        }
    }
}
