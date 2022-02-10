﻿
using System.Data;

namespace AutoGenerateDDDProjectForNetCore.models
{
    public enum SQLDbType
    {
        //
        // Summary:
        //     System.Int64. A 64-bit signed integer.
        BigInt = 127,
        //
        // Summary:
        //     System.Array of type System.Byte. A fixed-length stream of binary data ranging
        //     between 1 and 8,000 bytes.
        Binary = 173,
        //
        // Summary:
        //     System.Boolean. An unsigned numeric value that can be 0, 1, or null.
        Bit = 104,
        //
        // Summary:
        //     System.String. A fixed-length stream of non-Unicode characters ranging between
        //     1 and 8,000 characters.
        Char = 175,
        //
        // Summary:
        //     System.DateTime. Date and time data ranging in value from January 1, 1753 to
        //     December 31, 9999 to an accuracy of 3.33 milliseconds.
        DateTime = 61,
        //
        // Summary:
        //     System.Decimal. A fixed precision and scale numeric value between -10 38 -1 and
        //     10 38 -1.
        Decimal = 106,
        //
        // Summary:
        //     System.Double. A floating point number within the range of -1.79E +308 through
        //     1.79E +308.
        Float = 62,
        //
        // Summary:
        //     System.Array of type System.Byte. A variable-length stream of binary data ranging
        //     from 0 to 2 31 -1 (or 2,147,483,647) bytes.
        Image = 34,
        //
        // Summary:
        //     System.Int32. A 32-bit signed integer.
        Int = 56,
        //
        // Summary:
        //     System.Decimal. A currency value ranging from -2 63 (or -9,223,372,036,854,775,808)
        //     to 2 63 -1 (or +9,223,372,036,854,775,807) with an accuracy to a ten-thousandth
        //     of a currency unit.
        Money = 60,
        //
        // Summary:
        //     System.String. A fixed-length stream of Unicode characters ranging between 1
        //     and 4,000 characters.
        NChar = 239,
        //
        // Summary:
        //     System.String. A variable-length stream of Unicode data with a maximum length
        //     of 2 30 - 1 (or 1,073,741,823) characters.
        NText = 99,
        //
        // Summary:
        //     System.String. A variable-length stream of Unicode characters ranging between
        //     1 and 4,000 characters. Implicit conversion fails if the string is greater than
        //     4,000 characters. Explicitly set the object when working with strings longer
        //     than 4,000 characters. Use System.Data.SQLDbType.NVarChar when the database column
        //     is nvarchar(max).
        NVarChar = 231,
        //
        // Summary:
        //     System.Single. A floating point number within the range of -3.40E +38 through
        //     3.40E +38.
        Real = 59,
        //
        // Summary:
        //     System.Guid. A globally unique identifier (or GUID).
        UniqueIdentifier = 36,
        //
        // Summary:
        //     System.DateTime. Date and time data ranging in value from January 1, 1900 to
        //     June 6, 2079 to an accuracy of one minute.
        SmallDateTime = 58,
        //
        // Summary:
        //     System.Int16. A 16-bit signed integer.
        SmallInt = 52,
        //
        // Summary:
        //     System.Decimal. A currency value ranging from -214,748.3648 to +214,748.3647
        //     with an accuracy to a ten-thousandth of a currency unit.
        SmallMoney = 122,
        //
        // Summary:
        //     System.String. A variable-length stream of non-Unicode data with a maximum length
        //     of 2 31 -1 (or 2,147,483,647) characters.
        Text = 35,
        //
        // Summary:
        //     System.Array of type System.Byte. Automatically generated binary numbers, which
        //     are guaranteed to be unique within a database. timestamp is used typically as
        //     a mechanism for version-stamping table rows. The storage size is 8 bytes.
        Timestamp = 189,
        //
        // Summary:
        //     System.Byte. An 8-bit unsigned integer.
        TinyInt = 48,
        //
        // Summary:
        //     System.Array of type System.Byte. A variable-length stream of binary data ranging
        //     between 1 and 8,000 bytes. Implicit conversion fails if the byte array is greater
        //     than 8,000 bytes. Explicitly set the object when working with byte arrays larger
        //     than 8,000 bytes.
        VarBinary = 21,
        //
        // Summary:
        //     System.String. A variable-length stream of non-Unicode characters ranging between
        //     1 and 8,000 characters. Use System.Data.SQLDbType.VarChar when the database column
        //     is varchar(max).
        VarChar = 167,
        //
        // Summary:
        //     System.Object. A special data type that can contain numeric, string, binary,
        //     or date data as well as the SQL Server values Empty and Null, which is assumed
        //     if no other type is declared.
        Variant = 23,
        //
        // Summary:
        //     An XML value. Obtain the XML as a string using the System.Data.SqlClient.SqlDataReader.GetValue(System.Int32)
        //     method or System.Data.SqlTypes.SqlXml.Value property, or as an System.Xml.XmlReader
        //     by calling the System.Data.SqlTypes.SqlXml.CreateReader method.
        Xml = 241,
        //
        // Summary:
        //     A SQL Server user-defined type (UDT).
        Udt = 29,
        //
        // Summary:
        //     A special data type for specifying structured data contained in table-valued
        //     parameters.
        Structured = 30,
        //
        // Summary:
        //     Date data ranging in value from January 1,1 AD through December 31, 9999 AD.
        Date = 40,
        //
        // Summary:
        //     Time data based on a 24-hour clock. Time value range is 00:00:00 through 23:59:59.9999999
        //     with an accuracy of 100 nanoseconds. Corresponds to a SQL Server time value.
        Time = 41,
        //
        // Summary:
        //     Date and time data. Date value range is from January 1,1 AD through December
        //     31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an accuracy
        //     of 100 nanoseconds.
        DateTime2 = 42,
        //
        // Summary:
        //     Date and time data with time zone awareness. Date value range is from January
        //     1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999
        //     with an accuracy of 100 nanoseconds. Time zone value range is -14:00 through
        //     +14:00.
        DateTimeOffset = 43
    }
    public class ColumnModel
    {
        public string Name { get; set; }
        public SQLDbType TypeData { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsNullable { get; set; }
        public int MaxLenght { get; set; }

        public static string GetTypeToCSharpTypeString(SQLDbType sqlType, bool isNullable)
        {
            switch (sqlType)
            {
                case SQLDbType.BigInt:
                    return isNullable ? "long?" : "long";

                case SQLDbType.Binary:
                case SQLDbType.Image:
                case SQLDbType.Timestamp:
                case SQLDbType.VarBinary:
                    return "byte[]";

                case SQLDbType.Bit:
                    return isNullable ? "bool?" : "bool";

                case SQLDbType.Char:
                case SQLDbType.NChar:
                case SQLDbType.NText:
                case SQLDbType.NVarChar:
                case SQLDbType.Text:
                case SQLDbType.VarChar:
                case SQLDbType.Xml:
                    return "string";

                case SQLDbType.DateTime:
                case SQLDbType.SmallDateTime:
                case SQLDbType.Date:
                case SQLDbType.Time:
                case SQLDbType.DateTime2:
                    return isNullable ? "DateTime?" : "DateTime";

                case SQLDbType.Decimal:
                case SQLDbType.Money:
                case SQLDbType.SmallMoney:
                    return isNullable ? "decimal?" : "decimal";

                case SQLDbType.Float:
                    return isNullable ? "double?" : "double";

                case SQLDbType.Int:
                    return isNullable ? "int?" : "int";

                case SQLDbType.Real:
                    return isNullable ? "float?" : "float";

                case SQLDbType.UniqueIdentifier:
                    return isNullable ? "Guid?" : "Guid";

                case SQLDbType.SmallInt:
                    return isNullable ? "short?" : "short";

                case SQLDbType.TinyInt:
                    return isNullable ? "byte?" : "byte";

                case SQLDbType.Variant:
                case SQLDbType.Udt:
                    return "object";

                case SQLDbType.Structured:
                    return "DataTable";

                case SQLDbType.DateTimeOffset:
                    return isNullable ? "DateTimeOffset?" : "DateTimeOffset";

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
    }
}