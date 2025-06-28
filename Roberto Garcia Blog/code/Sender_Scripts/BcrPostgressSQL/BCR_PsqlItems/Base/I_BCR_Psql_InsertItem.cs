using System;
using System.Linq;
using DataCollection.BcrPostgressSQL;
using Npgsql;

namespace DataCollection.Editor.PostgreSQL.BCR_PsqlItems
{
    public interface I_BCR_Psql_InsertItem
    {
        public string GetTable();

        public NpgsqlCommand CreateInsertCommand()
        {
            var insertCommand =
                new NpgsqlCommand(
                    $"INSERT INTO {GetTable()} ({string.Join(",", GetColumNames())}) VALUES {GetValuesString()}");
            SetParameters(ref insertCommand);
            return insertCommand;
        }
        public string CreateInsertCommandAsText()
        {
            var insertCommand =
                // new NpgsqlCommand($"INSERT INTO {GetTable()} ({string.Join(",", GetColumNames())}) VALUES {GetValuesNumberedPlaceholders()}");
            // SetParameters(ref insertCommand);
                $"{InsertClauseString()} VALUES {GetValuesString()}";
            return insertCommand;
        }
        
        private void SetParameters(ref NpgsqlCommand command)
        {
            var values = GetValues();
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];
                command.Parameters.AddWithValue($"${i + 1}", value);
            }
        }

        public string InsertClauseString()=>$"INSERT INTO {GetTable()} {GetColumnNamesJoined()}";
        public string GetColumnNamesJoined()=>$"({string.Join(",", GetColumNames())})";
        public string[] GetColumNames();
        public object[] GetValues();

        public string GetValuesString(bool includeParentheses=true)
        {
            var values = GetValues();
            var valuesCount = values.Length;
            var valuesString = includeParentheses?"(":"";
            for (int i = 0; i < valuesCount; i++)
            {
                // valuesString += $"${i + 1}";
                valuesString += $"{Bcr_PostgreSQLExtensions.FormatSqlValue(values[i])}";
                valuesString += i == valuesCount - 1 ? "" : ",";
            }
            if(includeParentheses)
                valuesString += ")";
            return valuesString;
        }
   
    }
}