namespace Consumer;

public static class Extensions
{
    public static string FormatSqlValue(object value)
    {
        if (value == null)
            return "NULL";

        return value switch
        {
            string s => $"'{s.Replace("'", "''")}'", // Convertir comillas en strings
            bool b => b ? "TRUE" : "FALSE", // PostgreSQL usa TRUE/FALSE para booleanos
            DateTimeOffset dt => $"'{dt:yyyy-MM-dd HH:mm:ss}'", // Formato SQL estándar
            TimeSpan t => $"'{t.ToString("c")}'",
            _ => value.ToString() // Para números y otros tipos
        };
    }
}