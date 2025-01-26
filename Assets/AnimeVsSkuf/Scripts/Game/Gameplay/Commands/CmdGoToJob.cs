using Game.State.CMD;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands
{
    public class CmdGoToJob : ICommand
    {
        public readonly int Id;

        public CmdGoToJob(int jobId)
        {
            Id = jobId;
        }
    }
}