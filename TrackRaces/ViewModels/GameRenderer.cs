using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrackRaces.Models;
using TrackRaces.ViewModels;

namespace TrackRaces.Logic
{
    public class GameRenderer
    {
        private Player Player1;
        private Player Player2;
        private GameSettings GameSettings;
        private Canvas GameCanvas;
        private Random random = new Random();
        public GameRenderer()
        {            
            
        }
        public void SetCanvas(Canvas canvas)
        {
            GameCanvas = canvas;
        }

        public void SetPlayers(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void SetGameSettings(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
        }

        public void ResetPlayerPosition()
        {            
            double canvasWidth = GameCanvas.ActualWidth;
            double canvasHeight = GameCanvas.ActualHeight;

            Player1.Position = new Point(canvasWidth * 0.25, canvasHeight * 0.5);
            Player2.Position = new Point(canvasWidth * 0.75, canvasHeight * 0.5);

            Player1.Angle = random.Next(0, 360);
            Player2.Angle = random.Next(0, 360);
        }

        public void ResetPlayerBonus()
        {
            Player1.JumpCollected = false;
            Player2.JumpCollected = false;
        }
        public void ResetPlayerScore()
        {
            Player1.Score = 0;
            Player2.Score = 0;
        }

        public void UpdatePlayerPositions(Player player1, Player player2)
        {
            MovePlayer(Player1);
            MovePlayer(Player2);            
        }

        private void MovePlayer(Player player)
        {
            double scalingFactor = 1/3.0;            
            double movement =  GameSettings.LineSpeed * scalingFactor;
            double radians = player.Angle * (Math.PI / 180); // Convert degrees to radians
            double newX = player.Position.X + movement * Math.Cos(radians);
            double newY = player.Position.Y + movement * Math.Sin(radians);

            DrawCircle(new Point(newX, newY), player.Color);
            player.Position = new Point(newX, newY);
        }

        private void DrawCircle(Point center, Color color)
        {
            double diameter = GameSettings.LineThickness;
            Brush brushColor = new SolidColorBrush(color);

            Ellipse circle = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Stroke = brushColor,
                StrokeThickness = GameSettings.LineThickness,
                Tag = "PlayerLine"
            };
            // Set position to center of the circle            
            Canvas.SetLeft(circle, center.X - diameter / 2);
            Canvas.SetTop(circle, center.Y - diameter / 2);

            GameCanvas.Children.Add(circle);
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

        public void RemovePlayerTracks()
        {
            // Find all lines with tag: PlayerLine
            var playerTracks = GameCanvas.Children
                .OfType<Ellipse>()
                .Where(ellipse => ellipse.Tag.ToString() == "PlayerLine")
                .ToList(); 
          
            foreach (var ellipse in playerTracks)
            {
                GameCanvas.Children.Remove(ellipse);
            }
        }
    }
}
