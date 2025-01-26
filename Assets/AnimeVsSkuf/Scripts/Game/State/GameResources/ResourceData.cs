using System;

namespace Game.State.GameResources
{
    [Serializable]
    public class ResourceData
    {
        public ResourceType ResourceType;
        public double Amount;
        public int MinValue;
        public int MaxValue;
    }
}