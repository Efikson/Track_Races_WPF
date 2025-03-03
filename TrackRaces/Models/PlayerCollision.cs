using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Numerics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Security.Cryptography.X509Certificates;
using TrackRaces.ViewModels;

namespace TrackRaces.Models
{
    public class PlayerCollision
    {
        private Player _player1;
        private Player _player2;
        private Canvas _gameCanvas;
        private GameSettings _gameSettings;
        private GameWindowViewModel _viewModel;
        private Random random = new Random();
        private RenderTargetBitmap _cachedBitmap;
        private readonly GameRenderer _gameRenderer;
        private readonly TimerManager _timerManager;

        private bool CollisionDetected = false;

        public event Action CollisionOccured;

        public PlayerCollision(GameRenderer gameRenderer, TimerManager timerManager)
        {
            _gameRenderer = gameRenderer;
            _timerManager = timerManager;    
        }

        public void SetPlayers(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void SetGameSettings(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void SetCanvas(Canvas canvas)
        {
            _gameCanvas = canvas;
        }

        public void SetViewModel(GameWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void CheckPlayerCollision(Player player)
        {
            double playerX = player.Position.X;
            double playerY = player.Position.Y;
            double playerAngle = player.Angle;
            string playerName = player.Name;

            CheckCollisionAtPoint(playerX, playerY, playerAngle);

            // Analyze collision via bitmap
            if (CollisionDetected)
            {
                Color pixelColor = GetPixelColor(playerX, playerY, playerAngle);
                ReactToCollision(player, playerName, pixelColor);
                CollisionDetected = false;
            }
        }

        public void CheckCollisionAtPoint(double playerX, double playerY, double playerAngle)
        {
            // Offset by line thickness
            double offset = _gameSettings.LineThickness * 0.75;

            // Coordinates of collision point in front of the player
            double radians = playerAngle * (Math.PI / 180);
            int collisionX = (int)(playerX + offset * Math.Cos(radians));
            int collisionY = (int)(playerY + offset * Math.Sin(radians));

            Point testPoint = new Point(collisionX, collisionY);
            HitTestResult result = VisualTreeHelper.HitTest(_gameCanvas, testPoint);

            if (result != null)
            {
                if (result.VisualHit is Rectangle ||
                    result.VisualHit is Ellipse)
                {
                    CollisionDetected = true;
                }
            }
        }

        public Color GetPixelColor(double playerX, double playerY, double playerAngle)
        {
            // Offset by line thickness
            double offset = _gameSettings.LineThickness * 0.75;

            // Coordinates of collision point in front of the player
            double radians = playerAngle * (Math.PI / 180);
            int collisionX = (int)(playerX + offset * Math.Cos(radians));
            int collisionY = (int)(playerY + offset * Math.Sin(radians));

            int width = (int)_gameCanvas.ActualWidth;
            int height = (int)_gameCanvas.ActualHeight;

            // RenderTargetBitmap with canvas dimensions and standard DPI (96) 
            if (_cachedBitmap == null)
            {
                _cachedBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            }

            // Update bitmap contents
            _cachedBitmap.Render(_gameCanvas);

            // Each pixel has 4 bytes (BGRA)
            int stride = width * 4;
            byte[] pixelData = new byte[height * stride];
            _cachedBitmap.CopyPixels(pixelData, stride, 0);

            // Index for the desired pixel
            int index = collisionY * stride + collisionX * 4;
            if (index + 3 >= pixelData.Length)
                return Colors.Transparent;

            byte blue = pixelData[index];
            byte green = pixelData[index + 1];
            byte red = pixelData[index + 2];
            byte alpha = pixelData[index + 3];

            return Color.FromArgb(alpha, red, green, blue);
        }

        public void ReactToCollision(Player player, string playerName, Color pixelColor)
        {
            int randomMessage = random.Next(0, 2);

            if (pixelColor == Colors.Red)
            {
                if (randomMessage == 0)
                    MessageBox.Show(playerName + " got tricked.");
                else
                    MessageBox.Show(playerName + " hit the red thin line.");
                HandleCollision(player);
            }
            else if (pixelColor == Colors.Blue)
            {
                if (randomMessage == 0)
                    MessageBox.Show(playerName + " got fooled.");
                else
                    MessageBox.Show(playerName + " hit the blue track.");
                HandleCollision(player);
            }
            else if (pixelColor == Colors.Green)
            {
                if (randomMessage == 0)
                    MessageBox.Show(playerName + " wanted to go to windows.");
                else
                    MessageBox.Show(playerName + " hit the green border.");
                HandleCollision(player);
            }
            else if (pixelColor == Colors.Gold)
            {
                if (player == _player1)
                {
                    _player1.JumpCollected = true;
                }
                else if (player == _player2)
                {
                    _player2.JumpCollected = true;
                }

                _gameRenderer.RemoveBonus();
            }
        }

        public void HandleCollision(Player player)
        {
            if (player == _player1)
            {
                _player2.Score++;
            }
            else if (player == _player2)
            {
                _player1.Score++;
            }           
            CollisionOccured.Invoke();
            _timerManager.CountdownValue = "Press ENTER for a new round";
        }

        public void CheckJumpCollision(Player player)
        {
            string playerName;

            if (player == _player1)
            {
                playerName = _player1.Name;
            }
            else if (player == _player2)
            {
                playerName = _player2.Name;
            }
            else
            {
                return;
            }

            if (IsOutOfBounds(player.Position))
            {
                MessageBox.Show(playerName + " wanted to run away cowardly.");
                HandleCollision(player);
            }
        }

        private bool IsOutOfBounds(Point position)
        {
            return position.X < 0 || position.Y < 0 ||
                   position.X > _gameCanvas.ActualWidth ||
                   position.Y > _gameCanvas.ActualHeight;
        }
    }
}
