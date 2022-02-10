namespace [$project_name].utils.helpers
{
    public enum StatusEnum
    {
        Active = 0,
        Delete = 1,
        Block = 2,
        New = 3
    }

    public enum CategoryEnum
    {
        Category = 0, // for menu
        Subject = 1, // for post
        Diary = 2, // for diary
    }

    public static class DataHelper
    {
        // Extension method, call for any object, eg "if (x.IsNumeric())..."
        public static bool IsNumeric(this object x)
        { return (x == null ? false : IsNumeric(x.GetType())); }

        // Method where you know the type of the object
        public static bool IsNumeric(Type type)
        { return IsNumeric(type, Type.GetTypeCode(type)); }

        // Method where you know the type and the type code of the object
        public static bool IsNumeric(Type type, TypeCode typeCode)
        { return (typeCode == TypeCode.Decimal || (type.IsPrimitive && typeCode != TypeCode.Object && typeCode != TypeCode.Boolean && typeCode != TypeCode.Char)); }

        public static bool IsSimpleType(this object x)
        { return (x == null ? false : IsSimpleType(x.GetType())); }

        public static bool IsSimpleType(Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]))
                ;
        }
    }
}