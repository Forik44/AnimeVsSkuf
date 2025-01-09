using AnimeVsSkuf.Scripts.Game.Settings;
using GoogleSpreadsheets;

namespace Game
{
    public class MainMenuEnterParams  : SceneEnterParams
    {
        public string Result { get; }

        public MainMenuEnterParams(string result) : base(Scenes.MAIN_MENU)
        {
            Result = result;
        }
    }
}