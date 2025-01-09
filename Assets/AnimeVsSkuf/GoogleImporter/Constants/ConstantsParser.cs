using System.Collections.Generic;
using AnimeVsSkuf.Scripts.Game.Settings;

namespace GoogleSpreadsheets
{
    public class ConstantsParser : IGoogleSheetsParser
    {
        private readonly GameSettings _gameSettings;
        private Constant _currentConstant;

        public ConstantsParser(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _gameSettings.Constants = new List<Constant>();
        }
        
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentConstant = new Constant()
                    {
                        Id = token
                    };
                    _gameSettings.Constants.Add(_currentConstant);
                    break;
                case "Value":
                    _currentConstant.Value = token;
                    break;
            }
        }
    }
}