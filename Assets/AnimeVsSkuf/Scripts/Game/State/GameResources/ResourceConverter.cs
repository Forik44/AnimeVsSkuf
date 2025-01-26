using System;

namespace Game.State.GameResources
{
    public class ResourceConverter
    {
        public ResourceType GetResourceType(string resourceName)
        {
            switch (resourceName)
            {
                case "Energy":
                    return ResourceType.Energy;
                case "Money":
                    return ResourceType.Money;
                case "Tonus":
                    return ResourceType.Tonus;
                case "Weight":
                    return ResourceType.Weight;
                case "DayEnergy":
                    return ResourceType.DayEnergy;
                default:
                    throw new Exception($"Unknown resource type {resourceName}");
                    return 0;
            }
        }
    }
}