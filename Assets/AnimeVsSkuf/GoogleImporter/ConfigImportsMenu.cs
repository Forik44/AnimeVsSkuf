using UnityEditor;

namespace GoogleSpreadsheets
{
    public class ConfigImportsMenu
    {
        private const string SPREADSHEET_ID = "1j5-xIGQ0GknTrKwmKHqufH5SM90RyK2uajweyTd9SYQ";
        private const string ITEMS_SHEETS_NAME = "Constants";
        private const string CREDENTIALS_PATH = "forikcorp-0ceef761658c";
        
        [MenuItem("ForikCorp/Import Configs")]
        private static async void LoadConfigs()
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            
            var constantsParser = new ConstantsParser();
            await sheetsImporter.DownloadAndParseSheet(ITEMS_SHEETS_NAME, constantsParser);
        }
    }
}