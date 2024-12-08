using R3;

namespace Game.State
{
    public class PlayerEntityProxy
    {
        public int Id { get; }
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<int> Level { get; }

        public PlayerEntityProxy(PlayerEntity playerEntity)
        {
            Id = playerEntity.Id;
            
            Name = new ReactiveProperty<string>(playerEntity.Name);
            Level = new ReactiveProperty<int>(playerEntity.Level);
            
            Name.Skip(1).Subscribe(value => playerEntity.Name = value);
            Level.Skip(1).Subscribe(value => playerEntity.Level = value);
        }
    }
}