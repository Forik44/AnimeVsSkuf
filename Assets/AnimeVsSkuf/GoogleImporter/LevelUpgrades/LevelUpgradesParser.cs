using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Settings;

namespace GoogleSpreadsheets.LevelUpgrades
{
    public class LevelUpgradesParser : IGoogleSheetsParser
    {
        private readonly GameSettings _gameSettings;
        private LevelUpgrade _currentLevelUpgrade;

        public LevelUpgradesParser(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameSettings.LevelUpgrades = new List<LevelUpgrade>();
        }
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Level":
                    _currentLevelUpgrade = new LevelUpgrade()
                    {
                        Level = int.Parse(token)
                    };
                    _gameSettings.LevelUpgrades.Add(_currentLevelUpgrade);
                    break;
                case "Cost":
                    _currentLevelUpgrade.Cost = int.Parse(token);
                    break;
            }
        }
    }
}