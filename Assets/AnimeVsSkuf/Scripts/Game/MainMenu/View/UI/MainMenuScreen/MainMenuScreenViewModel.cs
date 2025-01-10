using MVVM.UI;
using R3;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen
{
    public class MainMenuScreenViewModel : WindowViewModel
    {
        private readonly MainMenuUIManager _uiManager;
        public override string Id => "MainMenu/MainMenuScreen";

        public MainMenuScreenViewModel(MainMenuUIManager uiManager)
        {
            _uiManager = uiManager;
        }
        public void RequestOpenPlayersPopup()
        {
            _uiManager.OpenPlayersPopup();
        }
    }
}