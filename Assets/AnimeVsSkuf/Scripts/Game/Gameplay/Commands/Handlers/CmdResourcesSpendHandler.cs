using System;
using System.Linq;
using Game.State;
using Game.State.CMD;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdResourcesSpendHandler : ICommandHandler<CmdResourcesSpend>
    {
        private readonly PlayerEntityProxy _player;

        public CmdResourcesSpendHandler(PlayerEntityProxy player)
        {
            _player = player;
        }

        public bool Handle(CmdResourcesSpend command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = _player.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);
            if (requiredResource == null)
            {
                Debug.LogError("Trying to spend not existed resource");
                return false;
            }
            
            if (command.Amount < 0)
            {
                Debug.LogError("Spend resource amount is less than zero");
                return false;
            }
            
            int minValue = requiredResource.MinValue.CurrentValue;
            int maxValue = requiredResource.MaxValue.CurrentValue;
            
            if (requiredResource.Amount.Value - command.Amount < minValue && !command.CanClamp)
            {
                Debug.LogError(
                    $"Trying to spend more resources than can ({requiredResourceType}). Exists: {requiredResource.Amount.Value}, trying to spend: {command.Amount}, min:{minValue}");
                return false;
            }

            requiredResource.Amount.Value = Math.Clamp(requiredResource.Amount.Value - command.Amount, minValue, maxValue);

            return true;
        }
    }
}