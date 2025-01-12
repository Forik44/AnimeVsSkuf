using System.Linq;
using Game.State;
using Game.State.CMD;
using Game.State.GameResources;

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

            requiredResource.Amount.Value += command.Amount;

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