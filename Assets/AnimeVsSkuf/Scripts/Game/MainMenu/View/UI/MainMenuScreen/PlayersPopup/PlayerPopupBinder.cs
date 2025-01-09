using System.Collections.Generic;
using Game;
using Game.MainMenu;
using MVVM.UI;
using ObservableCollections;
using R3;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.PlayersPopup
{
    public class PlayerPopupBinder : PopupBinder<PlayersPopupViewModel>
    {
        [SerializeField] private PlayerBinder _prefabPlayer;
        [SerializeField] private Transform _cardContainer;
    
        private readonly Dictionary<int, PlayerBinder> _createdPlayerMap = new();
        private readonly CompositeDisposable _disposables = new();
        
        protected override void OnBind(PlayersPopupViewModel viewModel)
        {
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
}