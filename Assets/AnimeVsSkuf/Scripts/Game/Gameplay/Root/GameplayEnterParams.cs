namespace Game
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public int LevelNumber;
        
        public GameplayEnterParams(int levelNumber) : base(Scenes.GAMEPLAY)
        {
            LevelNumber = levelNumber;
        }
    }
}