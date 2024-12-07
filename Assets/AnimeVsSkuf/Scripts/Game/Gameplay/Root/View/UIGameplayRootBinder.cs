using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Game
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSignalSubject;
        public void GoToMainMenuButtonClick()
        {
            _exitSceneSignalSubject?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalSubject)
        {
            _exitSceneSignalSubject = exitSceneSignalSubject;
        }
    }
}
