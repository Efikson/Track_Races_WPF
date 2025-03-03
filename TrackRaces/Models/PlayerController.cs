using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TrackRaces.Views;

namespace TrackRaces.Models
{
    public class PlayerController
    {
        private Player _player1;
        private Player _player2;
        private double TurnSpeed { get; set; } = 5.0;
        private readonly GameRenderer _gameRenderer;
        private readonly PlayerCollision _playerCollision;

        public PlayerController(GameRenderer gameRenderer, PlayerCollision playerCollision)
        {
            _gameRenderer = gameRenderer;
            _playerCollision = playerCollision;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void UpdatePlayerMovements()
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                RotatePlayer(_player1, -TurnSpeed);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                RotatePlayer(_player1, TurnSpeed);
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                RotatePlayer(_player2, -TurnSpeed);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                RotatePlayer(_player2, TurnSpeed);
            }
            if (Keyboard.IsKeyDown(Key.Q))
            {
                _gameRenderer.ProcessJump(_player1);
                _playerCollision.CheckJumpCollision(_player1);
            }
            if (Keyboard.IsKeyDown(Key.NumPad0))
            {
                _gameRenderer.ProcessJump(_player2);
                _playerCollision.CheckJumpCollision(_player2);
            }
        }

        private void RotatePlayer(Player player, double angleChange)
        {
            player.Angle += angleChange;
        }
    }
}
