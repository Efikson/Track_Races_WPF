using System.Windows;
using System.Windows.Input;

namespace TrackRaces.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();                    
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
