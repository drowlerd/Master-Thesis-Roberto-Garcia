using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators
{
    public class BcrPgsqlMeleePieceGameplayBuilder:BcrPgsqlPieceGameplayBuilder<Bcr_Psql_melee_gameplay_metrics>
    {
        public BcrPgsqlMeleePieceGameplayBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_melee_gameplay_metrics CreateFrom(CombatPerPlayerDataCollection robotInfo)
        {
            return new Bcr_Psql_melee_gameplay_metrics()
            {
                times_used = robotInfo.MeleeTimesUsed,
                times_hit = robotInfo.MeleeTimesHit,
                damage_dealt = robotInfo.MeleeDamageDealt
            };
        }
    }
}