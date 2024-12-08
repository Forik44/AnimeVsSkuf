using Game.State;

namespace Game.MainMenu
{
    public class PlayerViewModel
    {
        private readonly PlayerEntityProxy _playerEntity;
        private readonly PlayersService _playersService;

        public PlayerViewModel(PlayerEntityProxy playerEntity, PlayersService playersService)
        {
            _playerEntity = playerEntity;
            _playersService = playersService;
        }
    }
}