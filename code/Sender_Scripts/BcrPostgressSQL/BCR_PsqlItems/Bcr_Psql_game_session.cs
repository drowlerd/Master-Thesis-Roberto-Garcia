using System;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_game_session:I_BCR_Psql_InsertItem
    {
        public int id;
        public int player_id;
        public DateTimeOffset session_start;
        // public DateTimeOffset session_end;
        public TimeSpan duration;
        
        public string GetTable()
        {
            return "game_session";
        }

        public string[] GetColumNames()
        {
            return new string[] { 
                // "id", 
                nameof(player_id),
                nameof(session_start),
                nameof(duration)};
        }

        public object[] GetValues()
        {
            return new object[]
            {
                // "id", 
                player_id,
                session_start,
                duration
            };
        }
    }
}