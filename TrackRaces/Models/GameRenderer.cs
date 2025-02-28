using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrackRaces.ViewModels;

namespace TrackRaces.Models
{
    public class GameRenderer
    {
        private Player _player1;
        private Player _player2;
        private GameSettings _gameSettings;
        private Canvas _gameCanvas;
        private Random random = new Random();
        public GameRenderer()
        {

        }
        public void SetCanvas(Canvas canvas)
        {
            _gameCanvas = canvas;
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

        public void ResetPlayerPosition()
        {
            double canvasWidth = _gameCanvas.ActualWidth;
            double canvasHeight = _gameCanvas.ActualHeight;

            _player1.Position = new Point(canvasWidth * 0.25, canvasHeight * 0.5);
            _player2.Position = new Point(canvasWidth * 0.75, canvasHeight * 0.5);

            _player1.Angle = random.Next(0, 360);
            _player2.Angle = random.Next(0, 360);
        }

        public void ResetPlayerBonus()
        {
            _player1.JumpCollected = false;
            _player2.JumpCollected = false;
            RemoveBonus();
        }

        public void ResetPlayerScore()
        {
            _player1.Score = 0;
            _player2.Score = 0;
        }

        public void UpdatePlayerPositions()
        {
            MovePlayer(_player1);
            MovePlayer(_player2);
        }

        private void MovePlayer(Player player)
        {
            double scalingFactor = 1 / 3.0;
            double movement = _gameSettings.LineSpeed * scalingFactor;
            double radians = player.Angle * (Math.PI / 180); // Convert degrees to radians
            double newX = player.Position.X + movement * Math.Cos(radians);
            double newY = player.Position.Y + movement * Math.Sin(radians);

            DrawCircle(new Point(newX, newY), player.Color);
            player.Position = new Point(newX, newY);
        }

        private void DrawCircle(Point center, Color color)
        {
            double diameter = _gameSettings.LineThickness;
            Brush brushColor = new SolidColorBrush(color);

            Ellipse circle = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Stroke = brushColor,
                StrokeThickness = _gameSettings.LineThickness,
                Tag = "PlayerLine"
            };
            // Set position to center of the circle            
            Canvas.SetLeft(circle, center.X - diameter / 2);
            Canvas.SetTop(circle, center.Y - diameter / 2);

            _gameCanvas.Children.Add(circle);
        }

        public void ProcessJump(Player player)
        {
            const int JumpRange = 50;

            if (player.JumpCollected)
            {
                double radians = player.Angle * (Math.PI / 180); // Convert degrees to radians
                double newX = player.Position.X + JumpRange * Math.Cos(radians);
                double newY = player.Position.Y + JumpRange * Math.Sin(radians);
                player.Position = new Point(newX, newY);
                player.JumpCollected = false;
            }
        }

        private Ellipse bonusShape;
        public void SpawnBonus()
        {
            if (bonusShape == null)
            {
                bonusShape = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Gold,
                    Tag = "Bonus"
                };
                _gameCanvas.Children.Add(bonusShape);
            }

            double x = random.Next(10, (int)(_gameCanvas.ActualWidth - bonusShape.Width - 10));
            double y = random.Next(10, (int)(_gameCanvas.ActualHeight - bonusShape.Height - 10));

            Canvas.SetLeft(bonusShape, x);
            Canvas.SetTop(bonusShape, y);
        }
        
        public void RemoveBonus()
        {
            _gameCanvas.Children.Remove(bonusShape);
            bonusShape = null;
        }

        public void RemovePlayerTracks()
        {
            // Find all lines with tag: PlayerLine
            var playerTracks = _gameCanvas.Children
                .OfType<Ellipse>()
                .Where(ellipse => ellipse.Tag.ToString() == "PlayerLine")
                .ToList();

            foreach (var ellipse in playerTracks)
            {
                _gameCanvas.Children.Remove(ellipse);
            }
        }
    }
}
