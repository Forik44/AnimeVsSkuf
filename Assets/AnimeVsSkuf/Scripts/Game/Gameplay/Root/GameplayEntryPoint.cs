using UnityEngine;
using System;
using R3;

namespace Game
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;

        public Observable<GameplayExitParams> Run(UIRootView uiRoot, GameplayEnterParams enterParams)
        {
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);
            
            var exitSceneSignalSubject = new Subject<Unit>();
            uiScene.Bind(exitSceneSignalSubject);
            
            Debug.Log($"GAMEPLAY ENTRY POINT: level = {enterParams.LevelNumber}");
            
            var mainMenuEnterParams = new MainMenuEnterParams("Win");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
            var exitToMainMenuSceneSignal = exitSceneSignalSubject.Select(_ => exitParams);

            return exitToMainMenuSceneSignal;
        }
    }
}