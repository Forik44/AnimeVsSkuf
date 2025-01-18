using Game.State;

namespace Game
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public int PlayerId;
        
        public GameplayEnterParams(int playerId) : base(Scenes.GAMEPLAY)
        {
            PlayerId = playerId;
        }
    }
}