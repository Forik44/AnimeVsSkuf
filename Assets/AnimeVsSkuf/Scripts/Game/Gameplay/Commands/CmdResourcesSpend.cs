using Game.State.CMD;
using Game.State.GameResources;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands
{
    public class CmdResourcesSpend : ICommand
    {
        public readonly ResourceType ResourceType;
        public readonly int Amount;

        public CmdResourcesSpend(ResourceType resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}