using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpreadsheets;
using GoogleSpreadsheets.LevelUpgrades;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Settings
{
    [Serializable]
    public class GameSettings
    {
        public List<Constant> Constants;
        public List<LevelUpgrade> LevelUpgrades;
        
        public string GetConstantValue(ConstantsType constantType)
        {
            return Constants.FirstOrDefault(e => e.Id == ConstantsConverter.GetConstantByType(constantType))?.Value;
        }

        public List<int> GetLevelUpgradesList()
        {
            var levelUpgradesList = new List<int>();
            int currentLevel = 2;
            foreach (var levelUpgrade in LevelUpgrades)
            {
                if (currentLevel != levelUpgrade.Level)
                {
                    throw new Exception("Level upgrades are not in order starting with 2");
                }

                currentLevel++;
                levelUpgradesList.Add(levelUpgrade.Cost);
            }
            
            return levelUpgradesList;
        }
    }
}