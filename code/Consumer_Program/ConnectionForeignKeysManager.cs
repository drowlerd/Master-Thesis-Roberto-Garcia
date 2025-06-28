using System.Text.RegularExpressions;

namespace Consumer;

public class ConnectionForeignKeysManager
{
    private Dictionary<string, object> _foreignKeysHandledDictionary;
    public int _gameSessionID;
    public int _currentMatchSetID;
    public int _currentMatchID;

    private static readonly string[] ForeignKeysHandled = new[] { "game_session_id", "match_set_id", "match_id" };
    private String SubsituteForeignKeyPattern => @"'\[fk:(\w+)\]'";

    public ConnectionForeignKeysManager()
    {
        _foreignKeysHandledDictionary = new Dictionary<string, object>();
        foreach (var foreignKey in ForeignKeysHandled)
            _foreignKeysHandledDictionary[foreignKey] = -1;
    }

    public void StoreValue(string foreignKeyName, object value)
    {
        if (_foreignKeysHandledDictionary.ContainsKey(foreignKeyName))
            _foreignKeysHandledDictionary[foreignKeyName] = value;
    }

    public void TryHandleForeignKey(ref string command)
    {
        command = Regex.Replace(command, SubsituteForeignKeyPattern, match =>
        {
            string key = match.Groups[1].Value; 
            Console.WriteLine($"foreign key found: {key}");
            return (_foreignKeysHandledDictionary.TryGetValue(key, out var value) ? Extensions.FormatSqlValue(value) : "NULL") ?? string.Empty; // Si no se encuentra, pone NULL
        });

    }
}