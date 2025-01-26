using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using AnimeVsSkuf.Scripts.Game.Gameplay.View.Jobs;
using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.PlayersPopup;
using DI;
using Game.MainMenu;
using Game.State;
using MVVM.UI;
using R3;

namespace Game
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<int> _exitSceneRequest;

        public GameplayUIManager(DIContainer container) : base(container)
        {
            _exitSceneRequest = container.Resolve<Subject<int>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
        }
        
        public GameplayScreenViewModel OpenScreenGameplay()
        {
            var viewModel = new GameplayScreenViewModel(this, _exitSceneRequest, Container.Resolve<PlayerEntityProxy>(),Container.Resolve<PlayersService>());
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }
        
        public JobsPopupViewModel OpenJobsPopup()
        {
            var jobsPopupViewModel = new JobsPopupViewModel(Container.Resolve<JobsService>());
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenPopup(jobsPopupViewModel);

            return jobsPopupViewModel;
        }
    }
}