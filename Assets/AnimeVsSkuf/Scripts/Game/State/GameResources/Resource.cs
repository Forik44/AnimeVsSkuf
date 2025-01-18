using R3;

namespace Game.State.GameResources
{
    public class Resource
    {
        public readonly ResourceData Origin;
        public readonly ReactiveProperty<int> Amount;
        public readonly ReadOnlyReactiveProperty<int> MinValue;
        public readonly ReadOnlyReactiveProperty<int> MaxValue;
        
        public ResourceType ResourceType => Origin.ResourceType;

        public Resource(ResourceData data)
        {
            Origin = data;
            Amount = new ReactiveProperty<int>(data.Amount);
            MinValue = new ReactiveProperty<int>(data.MinValue);
            MaxValue = new ReactiveProperty<int>(data.MaxValue);
            
            Amount.Subscribe(newValue => data.Amount = newValue);
            MinValue.Subscribe(newValue => data.MinValue = newValue);
            MaxValue.Subscribe(newValue => data.MaxValue = newValue);
        }
    }
}