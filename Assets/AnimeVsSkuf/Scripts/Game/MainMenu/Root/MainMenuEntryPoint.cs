using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        public Observable<MainMenuExitParams> Run(UIRootView uiRoot, MainMenuEnterParams enterParams)
        {
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSignalSubject = new Subject<Unit>();
            uiScene.Bind(exitSignalSubject);
            
            Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");
        
            var gameplayEnterParams = new GameplayEnterParams(3);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitToGameplaySceneSignal = exitSignalSubject.Select(_ => mainMenuExitParams);

            //если добавлять сцены то тут надо замёржить сигнал
            
            return exitToGameplaySceneSignal;
        }
    }
}
