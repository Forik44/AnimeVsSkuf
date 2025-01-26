using System;

namespace Game.State.Unlock
{
    [Serializable]
    public class PlayerDayMoreUnlock : IUnlockData
    {
        public PlayerDayMoreUnlock(string value)
        {
            UnlockType = UnlockType.PlayerDayMore;
            Value = value;
        }

        public override bool IsUnlocked(PlayerEntityProxy player) 
        {
            return player.Day.Value >= int.Parse(Value);
        }
    }
} 