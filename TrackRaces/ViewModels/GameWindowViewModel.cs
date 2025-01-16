using TrackRaces.Models;

namespace TrackRaces.ViewModels
{
    public class GameWindowViewModel
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        public GameSettings GameSettings { get; }

        public GameWindowViewModel(Player player1, Player player2, GameSettings gameSettings)
        {
            Player1 = player1;
            Player2 = player2;
            GameSettings = gameSettings;
        }
    }
}
