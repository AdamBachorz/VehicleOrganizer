using BachorzLibrary.Common.Extensions;

namespace VehicleOrganizer.DesktopApp.Utils
{
    public static class EnumUtils
    {
        public static TEnum ParseEnum<TEnum>(string item, bool isEnumDescription) where TEnum : Enum
        {
            try
            {
                if (isEnumDescription)
                {
                    foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
                    {
                        if (enumValue.Description().Equals(item))
                        {
                            return enumValue;
                        }
                    }
                    throw new ArgumentOutOfRangeException($"Description {item} doesnt match to any value of {typeof(TEnum).FullName} type");
                }
               
                return (TEnum)Enum.Parse(typeof(TEnum), item, true);
            }
            catch (Exception)
            {
                throw new ArgumentOutOfRangeException($"Value {item} cannot be parsed to {typeof(TEnum).FullName} enum type values");
            }
        }
    }
}
