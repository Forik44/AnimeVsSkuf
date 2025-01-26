using System;

namespace Game.State.Unlock
{
    public class UnlockProvider
    {
        public IUnlockData CreateUnlock(UnlockType unlockType, string value)
        {
            switch (unlockType)
            {
                case UnlockType.PlayerDayMore:
                    return new PlayerDayMoreUnlock(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unlockType), unlockType, null);
            }
        }
    }
}