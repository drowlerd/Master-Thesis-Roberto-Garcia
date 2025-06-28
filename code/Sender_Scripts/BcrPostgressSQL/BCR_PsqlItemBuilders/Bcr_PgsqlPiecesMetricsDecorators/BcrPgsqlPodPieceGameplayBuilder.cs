using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators
{
    public class BcrPgsqlPodPieceGameplayBuilder:BcrPgsqlPieceGameplayBuilder<Bcr_Psql_pod_gameplay_metrics>
    {
        public BcrPgsqlPodPieceGameplayBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_pod_gameplay_metrics CreateFrom(CombatPerPlayerDataCollection robotInfo)
        {
            return new Bcr_Psql_pod_gameplay_metrics()
            {
                times_used = robotInfo.PodTimesUsed,
                times_hit = robotInfo.PodTimesHit,
                damage_dealt = robotInfo.PodDamageDealt
            };
        }
    }
}