namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics
{
    public class Bcr_Psql_bomb_gameplay_metrics:Bcr_Psql_piece_gameplay_metrics
    {
        public override string GetTable()
        {
            return "bomb_gameplay_metrics";
        }
    }
}