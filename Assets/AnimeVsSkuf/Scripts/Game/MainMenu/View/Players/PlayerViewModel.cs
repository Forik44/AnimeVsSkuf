using Game.State;
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

        public PlayerViewModel(PlayerEntityProxy playerEntity, PlayersService playersService)
        {
            PlayerEntityId = playerEntity.Id;
            
            _playerEntity = playerEntity;
            _playersService = playersService;

            Name = playerEntity.Name;
            Level = playerEntity.Level;
        }
    }
}