using Game.State.CMD;

namespace Game.Gameplay
{
    public class CmdDeletePlayer : ICommand
    {
        public readonly int PlayerId;

        public CmdDeletePlayer(int playerId)
        {
            PlayerId = playerId;
        }
    }
}