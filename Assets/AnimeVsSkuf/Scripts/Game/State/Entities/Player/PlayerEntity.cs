using System;

namespace Game.State
{
    [Serializable]
    public class PlayerEntity : Entity
    {
        public int Id;
        public string Name;
        public int Level;
    }
}