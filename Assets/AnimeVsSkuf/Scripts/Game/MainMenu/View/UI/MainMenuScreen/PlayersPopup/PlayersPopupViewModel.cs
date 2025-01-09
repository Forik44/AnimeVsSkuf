using Game.MainMenu;
using MVVM.UI;
using ObservableCollections;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.PlayersPopup
{
    public class PlayersPopupViewModel : WindowViewModel
    {
        public override string Id => "MainMenu/Players/PlayersPopup";
        
        public readonly IObservableCollection<PlayerViewModel> AllPlayers;

        public PlayersPopupViewModel(PlayersService playersService)
        {
            AllPlayers = playersService.AllPlayers;
        }
    }
}