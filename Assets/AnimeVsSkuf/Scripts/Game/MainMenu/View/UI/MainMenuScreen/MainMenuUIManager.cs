using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.PlayersPopup;
using DI;
using Game;
using Game.MainMenu;
using MVVM.UI;
using R3;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen
{
    public class MainMenuUIManager : UIManager
    {
        public MainMenuUIManager(DIContainer container) : base(container)
        {

        }
        
        public MainMenuScreenViewModel OpenScreenMainMenu()
        {
            var viewModel = new MainMenuScreenViewModel(this);
            var rootUI = Container.Resolve<UIMainMenuRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public PlayersPopupViewModel OpenPlayersPopup()
        {
            var playersPopupViewModel = new PlayersPopupViewModel(Container.Resolve<PlayersService>());
            var rootUI = Container.Resolve<UIMainMenuRootViewModel>();

            rootUI.OpenPopup(playersPopupViewModel);

            return playersPopupViewModel;
        }
    }
}