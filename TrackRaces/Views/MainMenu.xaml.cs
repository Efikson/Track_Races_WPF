using System.Windows;
using System.Windows.Input;
using TrackRaces.ViewModels;
using TrackRaces.Models;
using TrackRaces.Views;
using Microsoft.Extensions.DependencyInjection;


namespace TrackRaces.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(MainMenuViewModel mainMenuViewModel)
        {
            InitializeComponent();
            // Set DataContext for binding
            DataContext = mainMenuViewModel;    
        }
    }
}
