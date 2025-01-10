using Game.State;
using Game.State.CMD;

namespace Game.Gameplay
{
    public class CmdCreatePlayerHandler : ICommandHandler<CmdCreatePlayer>
    {
        private readonly GameStateProxy _gameState;

        public CmdCreatePlayerHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }
        public bool Handle(CmdCreatePlayer command)
        {
            var playerId = _gameState.CreatePlayerId();
            var newPlayerEntity = new PlayerEntity
            {
                Id = playerId,
                Name = command.Name,
                Level = command.Level
            };
            
            var newPlayerEntityProxy = new PlayerEntityProxy(newPlayerEntity);
            _gameState.Players.Add(newPlayerEntityProxy);

            return true;
        }
    }
}