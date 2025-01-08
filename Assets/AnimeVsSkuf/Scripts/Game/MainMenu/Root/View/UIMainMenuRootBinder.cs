using System.Collections.Generic;
using Game;
using Game.MainMenu;
using ObservableCollections;
using R3;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    [SerializeField] private PlayerBinder _prefabPlayer;
    [SerializeField] private Transform _cardContainer;
    
    private Subject<Unit> _exitSceneSignalSubject;
    
    private readonly Dictionary<int, PlayerBinder> _createdPlayerMap = new();
    private readonly CompositeDisposable _disposables = new();
    public void GoToGameplayButtonClick()
    {
        _exitSceneSignalSubject.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubject, UIMainMenuRootViewModel viewModel)
    {
        _exitSceneSignalSubject = exitSceneSignalSubject;

        foreach (var playerViewModel in viewModel.AllPlayers)
        {
            CreatePlayer(playerViewModel);
        }
        
        _disposables.Add(viewModel.AllPlayers.ObserveAdd()
            .Subscribe(e => CreatePlayer(e.Value)));
        
        _disposables.Add(viewModel.AllPlayers.ObserveRemove()
            .Subscribe(e => DeletePlayer(e.Value)));
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    private void CreatePlayer(PlayerViewModel playerViewModel)
    {
        var createdPlayer = Instantiate(_prefabPlayer, _cardContainer);
        createdPlayer.Bind(playerViewModel);
        
        _createdPlayerMap[playerViewModel.PlayerEntityId] = createdPlayer;
    }

    private void DeletePlayer(PlayerViewModel playerViewModel)
    {
        if (_createdPlayerMap.TryGetValue(playerViewModel.PlayerEntityId, out var playerBinder))
        {
            Destroy(playerBinder.gameObject);
            _createdPlayerMap.Remove(playerViewModel.PlayerEntityId);
        }
    }
}
