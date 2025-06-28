using System.Text.RegularExpressions;

namespace Consumer;

using Npgsql;

public class Connection
{
    [Serializable]
    public class PostgreSqlConnection
    {
        public string host;
        private string username;
        private string password;
        private string database;


        public PostgreSqlConnection(string host, string username, string password, string database)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.database = database;
        }

        public NpgsqlConnectionStringBuilder ToNpgsqlConnectionStringBuilder()
        {
            return new NpgsqlConnectionStringBuilder()
            {
                Host = host,
                Username = username,
                Password = password,
                Database = database,
            };
        }

        public override string ToString()
        {
            return $"Host={host};Username={username};Password={password};Database={database}";
        }
    }

    private PostgreSqlConnection _connection;
    private ConnectionForeignKeysManager _foreignKeysManager;

    public Connection()
    {
        _foreignKeysManager = new ConnectionForeignKeysManager();
    }

    public void StartConnection()
    {
        _connection = new PostgreSqlConnection(host: "HOST", username: "USERNAME",
            password: "PASSWORD", database: "DATABASE");
    }

    public async Task Upload(string commandText)
    {
        try
        {
            
            _foreignKeysManager.TryHandleForeignKey(ref commandText);
            
            var query = ContainsReaderKeyword(commandText)
                ? HandleReadCommand(commandText)
                : HandleInsertCommand(commandText);
            
            await query;

        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"[DB] [ERROR] Exception while uploading query:> \n{commandText}\nException:{e.Message}|n");
        }
    }

    private async Task HandleInsertCommand(string commandText)
    {
        await using var dataSource = NpgsqlDataSource.Create(_connection.ToNpgsqlConnectionStringBuilder());
        await using var cmd = dataSource.CreateCommand(commandText);
        await cmd.ExecuteNonQueryAsync();
        
    }

    public async Task HandleReadCommand(string commandText)
    {
        MatchCollection matches = Regex.Matches(commandText, ReaderPattern);
        string targetForeignKey = matches[0].Groups[1].Value;

        var SQLCommand = commandText.Split("*")[2];

        await using var dataSource = NpgsqlDataSource.Create(_connection.ToNpgsqlConnectionStringBuilder());
        var readCommand = dataSource.CreateCommand(SQLCommand);
        
        await using var reader = await readCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            _foreignKeysManager.StoreValue(targetForeignKey, reader.GetValue(0));
        }
    }


    private bool ContainsReaderKeyword(string commandText)
    {
        return Regex.IsMatch(commandText, ReaderPattern);
    }

    string ReaderPattern => @"reader\[(\w+)\]";
}