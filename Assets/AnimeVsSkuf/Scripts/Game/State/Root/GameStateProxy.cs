using System.Linq;
using ObservableCollections;
using R3;

namespace Game.State
{
    public class GameStateProxy
    {
        public ObservableList<PlayerEntityProxy> Players { get; } = new();

        public GameStateProxy(GameState gameState)
        {
            gameState.Players.ForEach(player => Players.Add(new PlayerEntityProxy(player)));
            
            Players.ObserveAdd().Subscribe(e =>
            {
                var addedPlayerEntity = e.Value;
                gameState.Players.Add(new PlayerEntity
                {
                    Id = addedPlayerEntity.Id,
                    Name = addedPlayerEntity.Name.Value,
                    Level = addedPlayerEntity.Level.Value
                });
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