using Game.State;

namespace Game
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public PlayerEntityProxy Player;
        
        public GameplayEnterParams(PlayerEntityProxy player) : base(Scenes.GAMEPLAY)
        {
            Player = player;
        }
    }
}