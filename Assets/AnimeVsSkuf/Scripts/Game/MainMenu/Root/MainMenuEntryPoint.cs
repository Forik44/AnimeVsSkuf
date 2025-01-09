using System;
using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using DI;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        public Observable<MainMenuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)
        {
            MainMenuRegistrations.Register(mainMenuContainer, enterParams);
            var mainMenuViewModelsContainer = new DIContainer(mainMenuContainer);
            MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

            InitUI(mainMenuViewModelsContainer);
            
            Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");
        
            var gameplayEnterParams = new GameplayEnterParams(3);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitSignalSubject = mainMenuContainer.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
            var exitToGameplaySceneSignal = exitSignalSubject.Select(_ => mainMenuExitParams);

            //если добавлять сцены то тут надо замёржить сигнал
            
            return exitToGameplaySceneSignal;
        }

        private void InitUI(DIContainer viewsContainer)
        {
            var uiRoot = viewsContainer.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = viewsContainer.Resolve<UIMainMenuRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = viewsContainer.Resolve<MainMenuUIManager>();
            uiManager.OpenScreenMainMenu();
        }
    }
}
