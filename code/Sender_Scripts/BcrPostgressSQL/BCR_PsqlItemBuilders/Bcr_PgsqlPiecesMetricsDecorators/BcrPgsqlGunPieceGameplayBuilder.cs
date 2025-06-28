using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlPiecesGameplayMetrics;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators
{
    public class BcrPgsqlGunPieceGameplayBuilder:BcrPgsqlPieceGameplayBuilder<Bcr_Psql_gun_gameplay_metrics>
    {
        public BcrPgsqlGunPieceGameplayBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_gun_gameplay_metrics CreateFrom(CombatPerPlayerDataCollection robotInfo)
        {
            return new Bcr_Psql_gun_gameplay_metrics()
            {
                times_used = robotInfo.GunTimesUsed,
                times_hit = robotInfo.GunTimesHit,
                damage_dealt = robotInfo.GunDamageDealt
            };
        }
    }
}