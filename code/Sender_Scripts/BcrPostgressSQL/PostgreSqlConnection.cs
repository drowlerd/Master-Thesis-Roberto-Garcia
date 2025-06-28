using System;
using Npgsql;
using UnityEngine;

namespace DataCollection.BcrPostgressSQL
{
    [Serializable]
    public class PostgreSqlConnection
    {
        [SerializeField] public string host;
        [SerializeField] private string username;
        [SerializeField] private string password;
        [SerializeField] private string database;

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
}