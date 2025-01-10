using Game.MainMenu;
using MVVM.UI;
using ObservableCollections;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.PlayersPopup
{
    public class PlayersPopupViewModel : WindowViewModel
    {
        private readonly PlayersService _playersService;
        public override string Id => "MainMenu/Players/PlayersPopup";
        
        public readonly IObservableCollection<PlayerViewModel> AllPlayers;

        public PlayersPopupViewModel(PlayersService playersService)
        {
            _playersService = playersService;
            AllPlayers = playersService.AllPlayers;
        }

        public void CreatePlayer()
        {
            _playersService.CreatePlayer();
        }
    }
}