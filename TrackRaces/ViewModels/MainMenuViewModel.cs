using TrackRaces.Models;

namespace TrackRaces.ViewModels
{
    public class MainMenuViewModel
    {
        private GameSettings _gameSettings;

        public MainMenuViewModel(GameSettings gameSettings)
        {
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