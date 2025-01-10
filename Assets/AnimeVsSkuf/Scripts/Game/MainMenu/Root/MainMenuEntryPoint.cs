using System;
using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Common;
using AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.MainMenuScreen;
using DI;
using Game.State;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;
        
        private PlayerEntityProxy _player;

        public Observable<MainMenuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)
        {
            MainMenuRegistrations.Register(mainMenuContainer, enterParams);
            var mainMenuViewModelsContainer = new DIContainer(mainMenuContainer);
            MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

            InitUI(mainMenuViewModelsContainer);
            
            Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");
        
            var gameplayEnterParams = new GameplayEnterParams(_player);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            
            var exitSignalSubject = mainMenuContainer.Resolve<Subject<PlayerEntityProxy>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
            exitSignalSubject.Subscribe(player => 
                gameplayEnterParams.Player = player
                );
            
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
