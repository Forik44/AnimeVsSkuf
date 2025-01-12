using System;
using System.Collections.Generic;
using System.Linq;
using GoogleSpreadsheets;
using UnityEngine;

namespace AnimeVsSkuf.Scripts.Game.Settings
{
    [Serializable]
    public class GameSettings
    {
        public List<Constant> Constants;

        public string GetConstantValue(ConstantsType constantType)
        {
            return Constants.FirstOrDefault(e => e.Id == ConstantsConverter.GetConstantByType(constantType))?.Value;
        }
    }
}