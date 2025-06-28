using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators
{
    public class BcrPgsqlBcrPieceGameplayBuilder:BcrPgsqlPieceGameplayBuilder<Bcr_Psql_bcr_gameplay_metrics>
    {
        public BcrPgsqlBcrPieceGameplayBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_bcr_gameplay_metrics CreateFrom(CombatPerPlayerDataCollection robotInfo)
        {
            return new Bcr_Psql_bcr_gameplay_metrics()
            {
                times_used = robotInfo.BcrOVerdriveTimesUsed,
                times_hit = robotInfo.BcrOVerdriveTimesHit,
                damage_dealt = robotInfo.BcrOVerdriveDamageDealt
            };
        }
    }
}