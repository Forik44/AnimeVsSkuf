using System.Linq;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;
using UnityEngine;

namespace Game.Gameplay
{
    public class CmdStartNewDayHandler : ICommandHandler<CmdStartNewDay>
    {
        private readonly PlayerEntityProxy _player;
        
        private ResourcesService _resourcesService;

        public CmdStartNewDayHandler(PlayerEntityProxy player, ResourcesService resourcesService)
        {
            _player = player;
            _resourcesService = resourcesService;
        }
        public bool Handle(CmdStartNewDay command)
        {
            _player.Day.Value++;
            
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