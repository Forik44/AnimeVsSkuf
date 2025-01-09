using MVVM.UI;
using R3;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen
{
    public class MainMenuScreenViewModel : WindowViewModel
    {
        private readonly MainMenuUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;
        public override string Id => "MainMenu/MainMenuScreen";

        public MainMenuScreenViewModel(MainMenuUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = uiManager;
            _exitSceneRequest = exitSceneRequest;
        }
        public void RequestOpenPlayersPopup()
        {
            _uiManager.OpenPlayersPopup();
        }
    }
}