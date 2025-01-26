using System;
using System.Collections.Generic;
using System.Globalization;
using AnimeVsSkuf.Scripts.Game.Settings;
using Game.State.GameResources;
using Game.State.Jobs;
using Game.State.Unlock;

namespace GoogleSpreadsheets.Jobs
{
    public class JobsParser : IGoogleSheetsParser
    {
        private readonly GameSettings _gameSettings;
        private JobData _currentJob;
        private UnlockType _currentUnlockType;

        public JobsParser(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameSettings.Jobs = new List<JobData>();
        }
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentJob = new JobData()
                    {
                        Id = int.Parse(token)
                    };
                    _currentJob.CostResourceTypes = new List<ResourceType>();
                    _currentJob.CostResourceValues = new List<double>();
                    _currentJob.RewardResourceTypes = new List<ResourceType>();
                    _currentJob.RewardResourceValues = new List<double>();
                    _currentJob.Unlocks = new List<IUnlockData>();
                    _gameSettings.Jobs.Add(_currentJob);
                    break;
                case "Name":
                    _currentJob.Name = token;
                    break;
                case "CostResourceTypes":
                    List<string> costResourcesTypesFromConfig = new List<string>(token.Split(','));
                    foreach (string resourceType in costResourcesTypesFromConfig)
                    {
                        _currentJob.CostResourceTypes.Add(_gameSettings.ResourceConverter.GetResourceType(resourceType));   
                    }
                    break;
                case "CostResourceValues":
                    List<string> costResourcesValuesFromConfig = new List<string>(token.Split(','));
                    foreach (string resourceValue in costResourcesValuesFromConfig)
                    {
                        double value = double.Parse(resourceValue, new CultureInfo("en-US"));
                        _currentJob.CostResourceValues.Add(value);   
                    }
                    break;
                case "RewardResourceTypes":
                    List<string> rewardResourcesTypesFromConfig = new List<string>(token.Split(','));
                    foreach (string resourceType in rewardResourcesTypesFromConfig)
                    {
                        _currentJob.RewardResourceTypes.Add(_gameSettings.ResourceConverter.GetResourceType(resourceType));   
                    }
                    break;
                case "RewardResourceValues":
                    List<string> rewardResourcesValuesFromConfig = new List<string>(token.Split(','));
                    foreach (string resourceValue in rewardResourcesValuesFromConfig)
                    {
                        _currentJob.RewardResourceValues.Add(double.Parse(resourceValue, new CultureInfo("en-US")));   
                    }
                    break;
                case "UnlockTypes":
                    List<string> unlockTypesFromConfig = new List<string>(token.Split(','));
                    foreach (string unlockType in unlockTypesFromConfig)
                    {
                        _currentUnlockType = _gameSettings.UnlockConverter.GetUnlockType(unlockType);
                    }
                    break;
                case "UnlockValues":
                    List<string> unlockValuesFromConfig = new List<string>(token.Split(','));
                    foreach (string unlockValue in unlockValuesFromConfig)
                    {
                        IUnlockData unlock = _gameSettings.UnlockProvider.CreateUnlock(_currentUnlockType, unlockValue);
                        _currentJob.Unlocks.Add(unlock);
                    }
                    break;
                case "Experience":
                    _currentJob.Experience = int.Parse(token);
                    break;
            }
        }
    }
}