using System;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_match: I_BCR_Psql_InsertItem
    {
        public int id { get; set; }
        public string arena_id { get; set; }
        public int match_set_id { get; set; }
        public TimeSpan duration { get; set; }
        public string GetTable()
        {
            return "match";
        }

        public string[] GetColumNames()
        {
            // return new string[] { "id", "arena_id", "match_id", "duration", "player_count" };
            return new string[]
            {
                "arena_id", 
                "match_set_id", 
                "duration" 
            };
        }

        public object[] GetValues()
        {
           return new object[]
           {
               arena_id, 
               "[fk:match_set_id]", 
               duration
           };
        }
    }
}