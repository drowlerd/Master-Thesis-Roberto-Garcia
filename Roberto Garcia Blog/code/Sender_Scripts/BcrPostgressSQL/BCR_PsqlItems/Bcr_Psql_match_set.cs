using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_match_set:I_BCR_Psql_InsertItem
    {
        public int id {get; set;}
        public int session_id {get; set;}
        public int set_type {get; set;}
        public int match_duration_selected {get; set;}
        public bool rematch_pressed {get; set;}
        public bool all_matches_completed {get; set;}
        
        public string GetTable()
        {
            return "match_set";
        }

        public string[] GetColumNames()
        {
            return new []
            {
                // "id",
                "game_session_ID",
                "set_type",
                "match_duration_selected",
                "rematch_pressed",
                "all_matches_completed"
            };
        }

        public object[] GetValues()
        {
            return new object[]
            {
                // id,
                "[fk:game_session_id]",
                set_type,
                match_duration_selected,
                rematch_pressed,
                all_matches_completed
            };
        }
        
    }
}