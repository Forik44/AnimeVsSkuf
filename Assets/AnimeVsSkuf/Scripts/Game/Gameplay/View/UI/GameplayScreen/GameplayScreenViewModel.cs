using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using MVVM.UI;
using R3;

namespace Game
{
    public class GameplayScreenViewModel : WindowViewModel
    {
        public override string Id => "Gameplay/GameplayScreen";
        
        private readonly GameplayUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public GameplayScreenViewModel(GameplayUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = uiManager;
            _exitSceneRequest = exitSceneRequest;
            _exitSceneRequest = exitSceneRequest;
        }
        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}