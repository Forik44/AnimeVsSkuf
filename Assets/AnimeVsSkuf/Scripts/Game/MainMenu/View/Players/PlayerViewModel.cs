using Game.State;
using GoogleSpreadsheets;
using R3;

namespace Game.MainMenu
{
    public class PlayerViewModel
    {
        private readonly PlayerEntityProxy _playerEntity;
        private readonly string _defaultName;
        private readonly PlayersService _playersService;
        
        public readonly int PlayerEntityId;
        public readonly ReadOnlyReactiveProperty<string> Name;
        public readonly ReadOnlyReactiveProperty<int> Level;

        public PlayerViewModel(PlayerEntityProxy playerEntity, string defaultName,  PlayersService playersService)
        {
            PlayerEntityId = playerEntity.Id;
            
            _playerEntity = playerEntity;
            _defaultName = defaultName;
            _playersService = playersService;

            Name = playerEntity.Name;
            Level = playerEntity.Level;
        }
    }
}