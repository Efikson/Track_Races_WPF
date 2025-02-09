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

        public void UpdatePlayerPositions(Player player1, Player player2)
        {
            MovePlayer(Player1);
            MovePlayer(Player2);
        }

        private void MovePlayer(Player player)
        {
            double radians = player.Angle * (Math.PI / 180); // Convert degrees to radians
            double newX = player.Position.X + GameSettings.LineSpeed * 0.75 * Math.Cos(radians);
            double newY = player.Position.Y + GameSettings.LineSpeed * 0.75 * Math.Sin(radians);

            DrawLine(player.Position, new Point(newX, newY), player.Color);
            player.Position = new Point(newX, newY);
        }

        private void DrawLine(Point start, Point end, Color color)
        {
            Brush brushColor = new SolidColorBrush(color);
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = brushColor,
                StrokeThickness = GameSettings.LineThickness,
                Tag = "PlayerLine"
            };
            GameCanvas.Children.Add(line);
        }

        public void RemovePlayerTracks()
        {
            // Find all lines with tag: PlayerLine
            var playerLines = GameCanvas.Children
                .OfType<Line>()
                .Where(line => line.Tag.ToString() == "PlayerLine")
                .ToList(); 
          
            foreach (var line in playerLines)
            {
                GameCanvas.Children.Remove(line);
            }
        }

    }
}
