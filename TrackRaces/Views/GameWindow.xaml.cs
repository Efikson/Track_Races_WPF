using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TrackRaces.Logic;
using TrackRaces.ViewModels;

namespace TrackRaces.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {       
        public GameWindow()
        {
            InitializeComponent();  
        }

        private void NewRoundButton_Click (object sender, RoutedEventArgs e)
        {
            if (DataContext is GameWindowViewModel viewModel)
            {                
                viewModel.StartCountdownTimer();                
            }
        }
        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GameWindowViewModel viewModel)
            {                
                viewModel.StopAllTimers();
                viewModel.ReturnToMainMenu();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NewRoundButton_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                ReturnToMenu_Click(sender, e);
            }
        }

    }

}
