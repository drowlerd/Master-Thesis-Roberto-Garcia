using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators
{
    public class BcrPgsqlBombPieceGameplayBuilder:BcrPgsqlPieceGameplayBuilder<Bcr_Psql_bomb_gameplay_metrics>
    {
        public BcrPgsqlBombPieceGameplayBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_bomb_gameplay_metrics CreateFrom(CombatPerPlayerDataCollection robotInfo)
        {
            return new Bcr_Psql_bomb_gameplay_metrics()
            {
                times_used = robotInfo.BombTimesUsed,
                times_hit = robotInfo.BombTimesHit,
                damage_dealt = robotInfo.BombDamageDealt
            };
        }
    }
}