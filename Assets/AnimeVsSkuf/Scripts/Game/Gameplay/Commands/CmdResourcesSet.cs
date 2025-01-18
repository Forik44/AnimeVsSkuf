using Game.State.CMD;
using Game.State.GameResources;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands
{
    public class CmdResourcesSet : ICommand
    {
        public readonly ResourceType ResourceType;
        public readonly int Amount;
        public readonly bool CanClamp;

        public CmdResourcesSet(ResourceType resourceType, int amount, bool canClamp)
        {
            ResourceType = resourceType;
            Amount = amount;
            CanClamp = canClamp;
        }
    }
}