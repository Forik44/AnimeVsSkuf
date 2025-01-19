using System.Collections.Generic;
using System.IO;
using AnimeVsSkuf.Scripts.Game.Settings;
using GoogleSpreadsheets.LevelUpgrades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class ConfigImportsMenu
    {
        public static readonly string CONFIGS_PATH = "Assets/Configs";
        
        private const string SPREADSHEET_ID = "1j5-xIGQ0GknTrKwmKHqufH5SM90RyK2uajweyTd9SYQ";
        private const string CONSTANTS_SHEETS_NAME = "Constants";
        private const string LEVELUPGRADES_SHEETS_NAME = "LevelUpgrades";
        private const string CREDENTIALS_PATH = "forikcorp-0ceef761658c.json";
        
        [MenuItem("ForikCorp/Import Configs")]
        private static async void LoadConfigs()
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            var gameSettings = LoadSettings();
            
            var constantsParser = new ConstantsParser(gameSettings);
            await sheetsImporter.DownloadAndParseSheet(CONSTANTS_SHEETS_NAME, constantsParser);
            
            var levelUpgradesParser = new LevelUpgradesParser(gameSettings);
            await sheetsImporter.DownloadAndParseSheet(LEVELUPGRADES_SHEETS_NAME, levelUpgradesParser);

            SaveSettings(gameSettings);
        }

        private static void SaveSettings(GameSettings gameSettings)
        {
            File.WriteAllText(CONFIGS_PATH + "/GameSettings.json", JsonUtility.ToJson(gameSettings));
        }

        public static GameSettings LoadSettings()
        {
            GameSettings gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(CONFIGS_PATH + "/GameSettings.json"));
            if (gameSettings == null)
            {
                gameSettings = new GameSettings();
            }

            return gameSettings;
        }
    }
}