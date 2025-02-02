using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using TrackRaces.Models;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Numerics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrackRaces.ViewModels
{
    public class PlayerCollision
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        private Canvas gameCanvas;
        private readonly GameSettings gameSettings;     
        private readonly GameWindowViewModel viewModel;
        private Random random = new Random();
        public PlayerCollision(GameWindowViewModel viewModel, Canvas canvas, GameSettings settings, Player player1, Player player2)
        {
            this.viewModel = viewModel;
            gameCanvas = canvas;
            gameSettings = settings;
            Player1 = player1;
            Player2 = player2;
        }
        public void CheckPlayerCollision(int player)
        {
            double playerX, playerY, playerAngle;
            string playerName;
            
            if (player == 1)
            {
                playerX = Player1.Position.X;
                playerY = Player1.Position.Y;
                playerAngle = Player1.Angle;
                playerName = Player1.Name;
            }
            else if (player == 2)
            {
                playerX = Player2.Position.X;
                playerY = Player2.Position.Y;
                playerAngle = Player2.Angle;
                playerName = Player2.Name;
            }
            else
            {                
                return;
            }

            int randomMessage = random.Next(0, 2);

            Color pixelColor = GetPixelColor(playerX, playerY, playerAngle);

            // Collisions control by pixel color
            if (pixelColor == Colors.Red)
            {
                HandleCollison(player);
                if (randomMessage == 0)
                    MessageBox.Show(playerName + " got tricked.");
                else
                    MessageBox.Show(playerName + " hit the red thin line.");
            }
            else if (pixelColor == Colors.Blue)
            {
                HandleCollison(player);
                if (randomMessage == 0)
                    MessageBox.Show(playerName + " got fooled.");
                else
                    MessageBox.Show(playerName + " hit the blue track.");
            }
            else if (pixelColor == Colors.Green)
            {
                HandleCollison(player);
                if (randomMessage == 0)
                    MessageBox.Show(playerName + " wanted to go to windows.");
                else
                    MessageBox.Show(playerName + " hit the green border.");
            }
        }

        public Color GetPixelColor(double playerX, double playerY, double playerAngle)
        {
            // Offset by line thickness
            double offset = gameSettings.LineThickness * 3.0 / 4.0;

            // Coordinates of collision point in front of the player
            double radians = playerAngle * (Math.PI / 180); // Convert degrees to radians
            int collisionX = (int)Math.Round(playerX + offset * Math.Cos(radians));
            int collisionY = (int)Math.Round(playerY + offset * Math.Sin(radians));
            
            int width = (int)gameCanvas.ActualWidth;
            int height = (int)gameCanvas.ActualHeight;

            // RenderTargetBitmap with canvas dimensions and standard DPI (96)            
            RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            rtb.Render(gameCanvas);

            // Each pixel has 4 bytes (BGRA)
            int step = width * 4;
            byte[] pixelData = new byte[height * step];
            rtb.CopyPixels(pixelData, step, 0);

            // Index for the desired pixel
            int index = collisionY * step + collisionX * 4;
            if (index + 3 >= pixelData.Length)
                return Colors.Transparent;

            byte blue = pixelData[index];
            byte green = pixelData[index + 1];
            byte red = pixelData[index + 2];
            byte alpha = pixelData[index + 3];

            return Color.FromArgb(alpha, red, green, blue);
        }

        public void HandleCollison(int player)
        {
            if (player == 1)
            {
                Player1.Score++;
            }
            else if (player == 2)
            {
                Player2.Score++;
            }
            
            viewModel.StopAllTimers();

        }
    }
}
