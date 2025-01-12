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
                case ConstantsType.DefaultResourceMoney:
                    return "default_resource_money";
                case ConstantsType.DefaultResourceWeight:
                    return "default_resource_weight";
                case ConstantsType.DefaultResourceWeariness:
                    return "default_resource_weariness";
                
                default:
                    return "";
            }
        }
    }
    public enum ConstantsType
    {
        DefaultPlayerName,
        DefaultResourceMoney,
        DefaultResourceWeight,
        DefaultResourceWeariness
    }
}