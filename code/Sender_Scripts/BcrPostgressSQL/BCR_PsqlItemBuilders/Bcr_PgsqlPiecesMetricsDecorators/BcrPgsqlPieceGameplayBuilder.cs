using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators
{
    public abstract class BcrPgsqlPieceGameplayBuilder<T>:ABcrPsqlInsertItemBuilder<T,CombatPerPlayerDataCollection> where T: Bcr_Psql_piece_gameplay_metrics
    {
        public BcrPgsqlPieceGameplayBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }
    }
}