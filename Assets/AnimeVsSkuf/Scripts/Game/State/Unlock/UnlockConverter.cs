using System;

namespace Game.State.Unlock
{
    public class UnlockConverter
    {
        public UnlockType GetUnlockType(string unlockName)
        {
            switch (unlockName)
            {
                case "PlayerDayMore":
                    return UnlockType.PlayerDayMore;
                    break;
                
                default:
                    throw new Exception($"Unknown unlock type {unlockName}");
                    return 0;
            }
        }
    }
}