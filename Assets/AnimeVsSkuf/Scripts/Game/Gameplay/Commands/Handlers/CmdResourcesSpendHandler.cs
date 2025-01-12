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

            if (requiredResource.Amount.Value < command.Amount)
            {
                Debug.LogError(
                    $"Trying to spend more resources than existed ({requiredResourceType}). Exists: {requiredResource.Amount.Value}, trying to spend: {command.Amount}");
                return false;
            }

            requiredResource.Amount.Value -= command.Amount;

            return true;
        }
    }
}