namespace Game
{
    public class MainMenuExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public MainMenuExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}