namespace IOBBank.Core.Extensions
{
    public static class EnumExtension
    {
        public static bool BeAValidEnumValue<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }
    }
}
