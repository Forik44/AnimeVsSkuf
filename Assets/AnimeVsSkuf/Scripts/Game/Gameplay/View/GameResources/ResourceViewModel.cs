using Game.State.GameResources;
using R3;

namespace Game.GameResources
{
    public class ResourceViewModel
    {
        public readonly ResourceType ResourceType;
        public readonly ReadOnlyReactiveProperty<double> Amount;
        public readonly ReadOnlyReactiveProperty<int> MinValue;
        public readonly ReadOnlyReactiveProperty<int> MaxValue;

        public ResourceViewModel(Resource resource)
        {
            ResourceType = resource.ResourceType;
            Amount = resource.Amount;
            MinValue = resource.MinValue;
            MaxValue = resource.MaxValue;
        }
    }
}