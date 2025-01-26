using System;
using System.Collections.Generic;
using System.Linq;
using Game.State.GameResources;
using Game.State.Jobs;
using Game.State.Unlock;
using GoogleSpreadsheets;
using GoogleSpreadsheets.LevelUpgrades;
using Newtonsoft.Json;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Settings
{
    [Serializable]
    public class GameSettings
    {
        public List<Constant> Constants;
        public List<LevelUpgrade> LevelUpgrades;
        public List<JobData> Jobs;

        [JsonIgnore] public readonly ResourceConverter ResourceConverter;
        [JsonIgnore] public readonly UnlockConverter UnlockConverter;
        [JsonIgnore] public readonly UnlockProvider UnlockProvider;

        public GameSettings()
        {
            ResourceConverter = new ResourceConverter();
            UnlockConverter = new UnlockConverter();
            UnlockProvider = new UnlockProvider();
        }
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