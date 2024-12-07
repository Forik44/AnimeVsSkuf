using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubject;
    public void GoToGameplayButtonClick()
    {
        _exitSceneSignalSubject.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;
    }
}
