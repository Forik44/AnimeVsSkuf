using Game.State.CMD;

namespace Game.Gameplay
{
    public class CmdCreatePlayer : ICommand
    {
        public readonly string Name;
        public readonly int Level = 1;
        public readonly int Day = 1;

        public CmdCreatePlayer(string name)
        {
            Name = name;
        }
    }
}