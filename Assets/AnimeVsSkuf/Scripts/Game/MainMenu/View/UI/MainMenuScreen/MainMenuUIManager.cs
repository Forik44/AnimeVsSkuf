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
        private readonly Subject<Unit> _exitSceneRequest;

        public MainMenuUIManager(DIContainer container) : base(container)
        {
            _exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        }
        
        public MainMenuScreenViewModel OpenScreenMainMenu()
        {
            var viewModel = new MainMenuScreenViewModel(this, _exitSceneRequest);
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