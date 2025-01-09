using GoogleSpreadsheets;

namespace AnimeVsSkuf.Scripts.Game.Settings
{
    public class GameSettingsProvider
    {
        public GameSettings GameSettings { get; private set;}

        public void LoadGameSettings()
        {
            GameSettings = ConfigImportsMenu.LoadSettings();
        }
    }
}