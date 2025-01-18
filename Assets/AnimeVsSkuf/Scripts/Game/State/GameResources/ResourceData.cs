using System;

namespace Game.State.GameResources
{
    [Serializable]
    public class ResourceData
    {
        public ResourceType ResourceType;
        public int Amount;
        public int MinValue;
        public int MaxValue;
    }
}