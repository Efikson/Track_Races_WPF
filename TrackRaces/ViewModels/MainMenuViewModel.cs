using System.Windows.Media;
using TrackRaces.Models;
using TrackRaces.Views;

namespace TrackRaces.ViewModels
{
    public class MainMenuViewModel
    {        
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }        
        private GameSettings _gameSettings;

        public MainMenuViewModel(GameSettings gameSettings)
        {            
            // Initializing players with default values
            Player1 = new Player("Player One", Colors.Red);
            Player2 = new Player("Player Two", Colors.Blue);

            _gameSettings = gameSettings;
        }

        public GameSettings GameSettings
        {
            get => _gameSettings;
            set
            {
                _gameSettings = value;               
            }
        }
    }

}