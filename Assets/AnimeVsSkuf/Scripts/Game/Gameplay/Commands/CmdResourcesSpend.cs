using Game.State.CMD;
using Game.State.GameResources;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands
{
    public class CmdResourcesSpend : ICommand
    {
        public readonly ResourceType ResourceType;
        public readonly double Amount;
        public readonly bool CanClamp;

        public CmdResourcesSpend(ResourceType resourceType, double amount, bool canClamp)
        {
            ResourceType = resourceType;
            Amount = amount;
            CanClamp = canClamp;
        }
    }
}