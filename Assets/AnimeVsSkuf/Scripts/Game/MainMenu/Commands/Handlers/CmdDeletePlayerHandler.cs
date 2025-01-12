using System.Linq;
using Game.State;
using Game.State.CMD;

namespace Game.Gameplay
{
    public class CmdDeletePlayerHandler : ICommandHandler<CmdDeletePlayer>
    {
        private readonly GameStateProxy _gameState;

        public CmdDeletePlayerHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }
        public bool Handle(CmdDeletePlayer command)
        {
            _gameState.Players.Remove(_gameState.Players.FirstOrDefault(e => e.Id == command.PlayerId));

            return true;
        }
    }
}