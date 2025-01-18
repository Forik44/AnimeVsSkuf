using System;
using System.Linq;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdResourcesSetHandler : ICommandHandler<CmdResourcesSet>
    {
        private readonly PlayerEntityProxy _player;

        public CmdResourcesSetHandler(PlayerEntityProxy player)
        {
            _player = player;
        }
        
        public bool Handle(CmdResourcesSet command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = _player.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);
            if (requiredResource == null)
            {
                requiredResource = CreateNewResource(requiredResourceType);
            }

            int minValue = requiredResource.MinValue.CurrentValue;
            int maxValue = requiredResource.MaxValue.CurrentValue;
            
            if ((command.Amount < minValue || command.Amount > maxValue) && !command.CanClamp)
            {
                Debug.LogError(
                    $"Trying to set more or less resources than existed ({requiredResourceType}). Min: {requiredResource.MinValue.CurrentValue}, Max: {requiredResource.MaxValue.CurrentValue}, trying to set: {command.Amount}");
                return false;
            }
            
            requiredResource.Amount.Value = Math.Clamp(command.Amount, minValue, maxValue);
            return true;
        }

        private Resource CreateNewResource(ResourceType resourceType)
        {
            var newResourceData = new ResourceData
            {
                ResourceType = resourceType,
                Amount = 0
            };

            var newResource = new Resource(newResourceData);
            _player.Resources.Add(newResource);

            
            return newResource;
        }
    }
}