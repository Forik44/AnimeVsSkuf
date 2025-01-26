using System.Linq;
using AnimeVsSkuf.Scripts.Game.Gameplay.Services;
using Game.State;
using Game.State.CMD;

namespace AnimeVsSkuf.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdGoToJobHandler : ICommandHandler<CmdGoToJob>
    {
        private readonly PlayerEntityProxy _player;
        private readonly ResourcesService _resourcesService;

        public CmdGoToJobHandler(PlayerEntityProxy player, ResourcesService resourcesService)
        {
            _player = player;
            _resourcesService = resourcesService;
        }
        public bool Handle(CmdGoToJob command)
        {
            var job = _player.Jobs.FirstOrDefault(e => e.Id == command.Id);

            for (int i = 0; i < job.CostResourceTypes.Count; i++)
            {
                if (!_resourcesService.IsEnoughResources(job.CostResourceTypes[i], job.CostResourceValues[i]))
                    return false;
            }
            
            for (int i = 0; i < job.CostResourceTypes.Count; i++)
            {
               _resourcesService.TrySpendResource(job.CostResourceTypes[i], job.CostResourceValues[i]);
            }
            
            for (int i = 0; i < job.RewardResourceTypes.Count; i++)
            {
                _resourcesService.AddResource(job.RewardResourceTypes[i], job.RewardResourceValues[i]);
            }
            
            _player.Experience.Value += job.Experience.Value;
            
            return true;
        }
    }
}