using System;
using System.Linq;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdResourcesAddHandler : ICommandHandler<CmdResourcesAdd>
    {
        private readonly PlayerEntityProxy _player;

        public CmdResourcesAddHandler(PlayerEntityProxy player)
        {
            _player = player;
        }
        
        public bool Handle(CmdResourcesAdd command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = _player.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);
            if (requiredResource == null)
            {
                requiredResource = CreateNewResource(requiredResourceType);
            }

            if (command.Amount < 0)
            {
                Debug.LogError("Add resource amount is less than zero");
                return false;
            }
            
            int minValue = requiredResource.MinValue.CurrentValue;
            int maxValue = requiredResource.MaxValue.CurrentValue;
            
            if (command.Amount + requiredResource.Amount.Value > maxValue && !command.CanClamp)
            {
                Debug.LogError(
                    $"Trying to add more resources than can ({requiredResourceType}). Exists: {requiredResource.Amount.Value}, trying to add: {command.Amount}, max: {maxValue}");
                return false;
            }

            requiredResource.Amount.Value = Math.Clamp(requiredResource.Amount.Value + command.Amount, minValue, maxValue);
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