using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TrackRaces.ViewModels;

namespace TrackRaces.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {       
        public GameWindow(GameWindowViewModel gameWindowViewModel)
        {
            InitializeComponent();            

            DataContext = gameWindowViewModel; 
            
            gameWindowViewModel.SetCanvas(gameCanvas);
            gameWindowViewModel.SetGameRenderer();
            gameWindowViewModel.SetPlayerController();
        }

        private void NewRoundButton_Click (object sender, RoutedEventArgs e)
        {
            if (DataContext is GameWindowViewModel viewModel)
            {
                viewModel.StartCountdown();
                viewModel.StartBonusTimer();
            }
        }
        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GameWindowViewModel viewModel)
            {
                viewModel.ReturnToMainMenu();
            }
        }

    }

}
