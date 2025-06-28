using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics
{
    public abstract class Bcr_Psql_piece_gameplay_metrics:I_BCR_Psql_InsertItem
    {
        private int id;
        public int times_used;
        public  int times_hit;
        public int damage_dealt;
        
        public abstract string GetTable();
        public string[] GetColumNames()
        {
            return new string[]
            {
                nameof(times_used),
                nameof(times_hit),
                nameof(damage_dealt)
            };
        }

        public object[] GetValues()
        {
            return new object[]
            {
                times_used,
                times_hit,
                damage_dealt
            };
        }
    }
}