using System;
using System.Linq;

namespace DataCollection.BcrPostgressSQL
{
    public static class Bcr_PostgreSQLExtensions
    {
        public static string FormatSqlValue(object value)
        {
            if (value == null)
                return "NULL";

            return value switch
            {
                string s => $"'{s.Replace("'", "''")}'", // Convertir comillas en strings
                bool b => b ? "TRUE" : "FALSE", // PostgreSQL usa TRUE/FALSE para booleanos
                float f => $"'{f.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}'", // PostgreSQL usa TRUE/FALSE para booleanos
                DateTimeOffset dt => $"'{dt:yyyy-MM-dd HH:mm:ss}'",// Formato SQL estándar
                float[] fArray=> "'{"+$"{String.Join(',',fArray.Select(f=>f.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)))}"+"}'",
                TimeSpan t => $"'{t.ToString("c")}'",
                _ => value.ToString() // Para números y otros tipos
            };
        }

        public static string AddReaderKey(string readerTarget)
        {
            return $"*reader[{readerTarget}]* ";
        }
    }
}