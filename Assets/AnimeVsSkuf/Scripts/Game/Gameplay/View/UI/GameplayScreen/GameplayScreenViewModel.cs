using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using Game.MainMenu;
using Game.State;
using MVVM.UI;
using R3;

namespace Game
{
    public class GameplayScreenViewModel : WindowViewModel
    {
        public override string Id => "Gameplay/GameplayScreen";
        public readonly ReadOnlyReactiveProperty<int> Level;
        public readonly ReadOnlyReactiveProperty<int> Day;
        
        private readonly GameplayUIManager _uiManager;
        private readonly Subject<int> _exitSceneRequest;
        private readonly PlayerEntityProxy _player;
        private readonly PlayersService _playersService;

        public GameplayScreenViewModel(GameplayUIManager uiManager, Subject<int> exitSceneRequest, PlayerEntityProxy player, PlayersService playersService)
        {
            Level = player.Level;
            Day = player.Day;
            
            _uiManager = uiManager;
            _exitSceneRequest = exitSceneRequest;
            _player = player;
            _playersService = playersService;
        }

        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(_player.Id);
        }

        public void RequestNextDay()
        {
            _playersService.StartNextDay();
        }
        
        public void RequestOpenPlayersPopup()
        {
            _uiManager.OpenJobsPopup();
        }
    }
}