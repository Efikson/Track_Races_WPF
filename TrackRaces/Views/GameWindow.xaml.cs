using System.Windows;
using System.Windows.Input;
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
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }

}
