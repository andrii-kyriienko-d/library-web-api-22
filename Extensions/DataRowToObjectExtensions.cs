using System;
using System.Data;
using System.Linq;

namespace LibraryWebApi.Extensions;

internal static class DataRowToObjectExtensions
{
    public static T ToObject<T>(this DataRow dataRow)
        where T : new()
    {
        var item = new T();
        var properties = item.GetType().GetProperties().ToList();
        foreach (DataColumn column in dataRow.Table.Columns)
        {
            if (dataRow[column] != DBNull.Value)
            {
                var prop = properties
                    .FirstOrDefault(x => x.Name == column.ColumnName);
                object result = Convert.ChangeType(dataRow[column], prop.PropertyType);
                prop.SetValue(item, result, null);
            }
        }
        return item;
    }
}