using Game.State;
using GoogleSpreadsheets;
using R3;

namespace Game.MainMenu
{
    public class PlayerViewModel
    {
        private readonly PlayerEntityProxy _playerEntity;
        private readonly PlayersService _playersService;
        
        public readonly int PlayerEntityId;
        public readonly ReadOnlyReactiveProperty<string> Name;
        public readonly ReadOnlyReactiveProperty<int> Level;
        private readonly Subject<int> _exitSceneRequest;

        public PlayerViewModel(PlayerEntityProxy playerEntity,  PlayersService playersService, Subject<int> exitSceneRequest)
        {
            PlayerEntityId = playerEntity.Id;
            
            _playerEntity = playerEntity;
            _playersService = playersService;
            _exitSceneRequest = exitSceneRequest;

            Name = playerEntity.Name;
            Level = playerEntity.Level;
        }

        public void RequestDeletePlayer()
        {
            _playersService.DeletePlayer(PlayerEntityId);
        }

        public void RequestStartGameplay()
        {
            _exitSceneRequest.OnNext(_playerEntity.Id);
        }
    }
}