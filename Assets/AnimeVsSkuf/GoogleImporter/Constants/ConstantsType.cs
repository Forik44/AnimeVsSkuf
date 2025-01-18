namespace GoogleSpreadsheets
{
    public class ConstantsConverter
    {
        public static string GetConstantByType(ConstantsType constantTypeType)
        {
            switch (constantTypeType)
            {
                case ConstantsType.DefaultPlayerName:
                    return "player_default_name";
                case ConstantsType.ResourceDefaultMoney:
                    return "resource_default_money";
                case ConstantsType.ResourceDefaultWeight:
                    return "resource_default_weight";
                case ConstantsType.ResourceDefaultTonus:
                    return "resource_default_tonus";
                case ConstantsType.ResourceDefaultEnergy:
                    return "resource_default_energy";
                case ConstantsType.ResourceDefaultDayEnergy:
                    return "resource_default_day_energy";
                case ConstantsType.ResourceMinWeight:
                    return "resource_min_weight";
                case ConstantsType.ResourceMaxDayEnergy:
                    return "resource_max_day_energy";
                
                default:
                    return "";
            }
        }
    }
    public enum ConstantsType
    {
        DefaultPlayerName,
        ResourceDefaultMoney,
        ResourceDefaultWeight,
        ResourceDefaultTonus,
        ResourceDefaultEnergy,
        ResourceDefaultDayEnergy,
        ResourceMinWeight,
        ResourceMaxDayEnergy
    }
}