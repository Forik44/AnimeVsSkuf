using System;
using UnityEngine;

namespace Game.State.Unlock
{
    [Serializable]
    public abstract class IUnlockData
    {
        public UnlockType UnlockType { get; protected set; }
        public string Value { get; protected set; }

        public virtual bool IsUnlocked(PlayerEntityProxy playerEntityProxy)
        {
            return false;
        }
    }
}