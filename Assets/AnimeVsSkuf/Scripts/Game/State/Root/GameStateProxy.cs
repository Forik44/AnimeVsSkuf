using System.Linq;
using Game.State.GameResources;
using ObservableCollections;
using R3;

namespace Game.State
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public ObservableList<PlayerEntityProxy> Players { get; } = new();

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;
            
            InitPlayers(gameState);
        }

        public int CreatePlayerId()
        {
            return _gameState.CreatePlayerId();
        }

        private void InitPlayers(GameState gameState)
        {
            gameState.Players.ForEach(player => Players.Add(new PlayerEntityProxy(player)));
            
            Players.ObserveAdd().Subscribe(e =>
            {
                var addedPlayerEntity = e.Value;
                gameState.Players.Add(addedPlayerEntity.Origin);
            });

            Players.ObserveRemove().Subscribe(e =>
            {
                var removedPlayerEntityProxy = e.Value;
                var removedPlayerEntity =
                    gameState.Players.FirstOrDefault(player => player.Id == removedPlayerEntityProxy.Id);
                gameState.Players.Remove(removedPlayerEntity);
            });
        }
    }
}