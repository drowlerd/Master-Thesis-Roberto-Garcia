using System.Collections.Generic;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_versus_AI_robot:I_BCR_Psql_InsertItem
    {
        public int AI_difficulty {get;set;}
        public float top_threshold {get;set;}
        public float bottom_threshold { get;set;}
        public string GetTable()
        {
            return "versus_ai_robot";
        }

        public string[] GetColumNames()  
        {
            return new[] {"difficulty", "top_threshold", "bottom_threshold"};
        }

        public object[] GetValues()
        {
            return new object[] { AI_difficulty, top_threshold, bottom_threshold };
        }
    }
}