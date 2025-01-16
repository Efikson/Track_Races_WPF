using Microsoft.Extensions.DependencyInjection;
using System.Windows;
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
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ReturnToMenu_Click(sender, e);
        }
        private void NewRoundButton_Click (object sender, RoutedEventArgs e)
        {
            if (DataContext is GameWindowViewModel viewModel)
            {
                viewModel.StartCountdown();
            }
        }
        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainMenuWindow = App.ServiceProvider?.GetRequiredService<MainMenu>();
            mainMenuWindow.Show();
            this.Close();
        }

    }

}
