namespace GoogleSpreadsheets
{
    public class ConstantsConverter
    {
        public static string GetConstantByType(ConstantsType constantTypeType)
        {
            switch (constantTypeType)
            {
                case ConstantsType.DefaultPlayerName:
                    return "default_player_name";
                
                default:
                    return "";
            }
        }
    }
    public enum ConstantsType
    {
        DefaultPlayerName,
    }
}