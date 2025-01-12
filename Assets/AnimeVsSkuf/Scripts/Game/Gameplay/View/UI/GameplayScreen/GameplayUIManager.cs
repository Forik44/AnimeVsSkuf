using AnimeVsSkuf.Scripts.Game.Common;
using DI;
using MVVM.UI;
using R3;

namespace Game
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;

        public GameplayUIManager(DIContainer container) : base(container)
        {
            _exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        }
        
        public GameplayScreenViewModel OpenScreenGameplay()
        {
            var viewModel = new GameplayScreenViewModel(this, _exitSceneRequest);
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }
    }
}