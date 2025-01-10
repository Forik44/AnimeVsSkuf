using System.Collections.Generic;
using Game;
using Game.MainMenu;
using MVVM.UI;
using ObservableCollections;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AnimeVsSkuf.Scripts.Game.MainMenu.View.UI.PlayersPopup
{
    public class PlayersPopupBinder : PopupBinder<PlayersPopupViewModel>
    {
        [SerializeField] private PlayerBinder _prefabPlayer;
        [SerializeField] private Transform _cardContainer;
        [SerializeField] private Button _btnCreatePlayer;
    
        private readonly Dictionary<int, PlayerBinder> _createdPlayerMap = new();
        private readonly CompositeDisposable _disposables = new();
        
        private PlayersPopupViewModel _playersPopupViewModel;
        
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

            _playersPopupViewModel = viewModel;
        }
        
        protected virtual void Start()
        {
            base.Start();
            
            _btnCreatePlayer?.onClick.AddListener(OnCreatePlayerClick);
        }
        
        protected void OnCreatePlayerClick()
        {
            _playersPopupViewModel.CreatePlayer();
        }

        private void OnDestroy()
        {
            base.OnDestroy();
            
            _btnCreatePlayer?.onClick.RemoveListener(OnCreatePlayerClick);
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