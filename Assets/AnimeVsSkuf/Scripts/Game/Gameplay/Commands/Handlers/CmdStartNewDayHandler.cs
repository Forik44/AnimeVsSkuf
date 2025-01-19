using System.Linq;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using AnimeVsSkuf.Scripts.Game.Settings;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;
using GoogleSpreadsheets;
using UnityEngine;

namespace Game.Gameplay
{
    public class CmdStartNewDayHandler : ICommandHandler<CmdStartNewDay>
    {
        private readonly PlayerEntityProxy _player;
        
        private ResourcesService _resourcesService;
        private readonly GameSettings _gameSettings;

        public CmdStartNewDayHandler(PlayerEntityProxy player, ResourcesService resourcesService, GameSettings gameSettings)
        {
            _player = player;
            _resourcesService = resourcesService;
            _gameSettings = gameSettings;
        }
        public bool Handle(CmdStartNewDay command)
        {
            _player.Day.Value++;
            _player.Experience.Value += int.Parse(_gameSettings.GetConstantValue(ConstantsType.DayExperienceIncome));
            
            var tonusResourceType = ResourceType.Tonus;
            var tonusResource = _player.Resources.FirstOrDefault(r => r.ResourceType == tonusResourceType);
            
            if (tonusResource == null)
            {
                Debug.LogError("Trying to spend not existed resource");
                return false;
            }            
            
            if (tonusResource.Amount.Value > 0)
            {
                _resourcesService.AddResource(ResourceType.DayEnergy, tonusResource.Amount.Value);
            }
            else
            {
                _resourcesService.TrySpendResource(ResourceType.DayEnergy, tonusResource.Amount.Value, true);
            }

            var resourceDayEnergy = _resourcesService.Resources
                .FirstOrDefault(r => r.ResourceType == ResourceType.DayEnergy);
            
            if (resourceDayEnergy == null)
            {
                Debug.LogError("Trying to spend not existed resource");
                return false;
            }   
            
            _resourcesService.SetResource(ResourceType.Energy, resourceDayEnergy.Amount.CurrentValue, true);
            
            return true;
        }
    }
}