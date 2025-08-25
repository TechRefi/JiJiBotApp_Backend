using System.Data;
using System.Reflection;
using JiJiBotApp_Backend.Extensions;

namespace JiJiBotApp_Backend.Extensions
{
    public static class DataTableExtensions
    {
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var result = new List<T>();

            // Get all properties of T
            var properties = typeof(T).GetProperties()
                .Where(prop => prop.CanWrite)
                .ToList();

            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();

                foreach (var property in properties)
                {
                    // Check if the DataTable contains a column with this property name
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        // Get the value from the DataTable
                        var value = row[property.Name];

                        // Skip if the value is DBNull
                        if (value == DBNull.Value)
                            continue;

                        try
                        {
                            // Convert the value to the property type and set it
                            property.SetValue(item, ConvertValue(value, property.PropertyType));
                        }
                        catch (Exception)
                        {
                            // If conversion fails, try to handle common type mismatches
                            HandleTypeMismatch(item, property, value);
                        }
                    }
                }

                result.Add(item);
            }

            return result;
        }

        private static object ConvertValue(object value, Type targetType)
        {
            // Handle nullable types
            if (Nullable.GetUnderlyingType(targetType) != null)
            {
                if (value == null)
                    return null;

                targetType = Nullable.GetUnderlyingType(targetType);
            }

            // Handle enums
            if (targetType.IsEnum)
                return Enum.Parse(targetType, value.ToString());

            // Handle common type conversions
            return Convert.ChangeType(value, targetType);
        }

        private static void HandleTypeMismatch(object item, PropertyInfo property, object value)
        {
            try
            {
                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                // Handle string to bool
                if (targetType == typeof(bool) && value is string strVal)
                {
                    if (bool.TryParse(strVal, out bool boolResult))
                    {
                        property.SetValue(item, boolResult);
                        return;
                    }

                    // Other string-to-bool conversions
                    var normalized = strVal.ToLower().Trim();
                    if (normalized == "yes" || normalized == "y" || normalized == "1")
                        property.SetValue(item, true);
                    else if (normalized == "no" || normalized == "n" || normalized == "0")
                        property.SetValue(item, false);
                }

                // Handle string to datetime
                else if (targetType == typeof(DateTime) && value is string dateStr)
                {
                    if (DateTime.TryParse(dateStr, out DateTime dateResult))
                    {
                        property.SetValue(item, dateResult);
                    }
                }

                // Handle numeric conversions
                else if ((targetType == typeof(int) || targetType == typeof(long) ||
                        targetType == typeof(decimal) || targetType == typeof(double))
                        && value != null)
                {
                    if (decimal.TryParse(value.ToString(), out decimal decimalValue))
                    {
                        if (targetType == typeof(int))
                            property.SetValue(item, Convert.ToInt32(decimalValue));
                        else if (targetType == typeof(long))
                            property.SetValue(item, Convert.ToInt64(decimalValue));
                        else if (targetType == typeof(double))
                            property.SetValue(item, Convert.ToDouble(decimalValue));
                        else
                            property.SetValue(item, decimalValue);
                    }
                }
            }
            catch
            {
                // Unable to convert, leave property as default value
            }
        }
    }
}
