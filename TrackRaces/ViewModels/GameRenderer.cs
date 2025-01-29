using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TrackRaces.Models;

namespace TrackRaces.Logic
{
    public class GameRenderer
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        private Canvas gameCanvas;
        private readonly GameSettings gameSettings;

        public GameRenderer(Canvas canvas, GameSettings settings, Player player1, Player player2)
        {
            gameCanvas = canvas;
            gameSettings = settings;
            Player1 = player1;
            Player2 = player2;
        }

        public void ResetPlayerPosition()
        {
            Random random = new Random();
            double canvasWidth = gameCanvas.ActualWidth;
            double canvasHeight = gameCanvas.ActualHeight;

            Player1.Position = new Point(canvasWidth * 0.25, canvasHeight * 0.5);
            Player2.Position = new Point(canvasWidth * 0.75, canvasHeight * 0.5);

            Player1.Angle = random.Next(0, 360);
            Player2.Angle = random.Next(0, 360);
        }

        public void UpdatePlayerPositions(Player player1, Player player2)
        {
            MovePlayer(Player1);
            MovePlayer(Player2);
        }

        private void MovePlayer(Player player)
        {
            double radians = player.Angle * (Math.PI / 180); // Convert degrees to radians
            double newX = player.Position.X + gameSettings.LineSpeed * Math.Cos(radians);
            double newY = player.Position.Y + gameSettings.LineSpeed * Math.Sin(radians);

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
                StrokeThickness = gameSettings.LineThickness
            };
            gameCanvas.Children.Add(line);
        }
    }
}
