using Game.MainMenu;
using ObservableCollections;

namespace Game
{
    public class UIMainMenuRootViewModel
    {
        public readonly IObservableCollection<PlayerViewModel> AllPlayers;

        public UIMainMenuRootViewModel(PlayersService playersService)
        {
            AllPlayers = playersService.AllPlayers;
        }
    }
}