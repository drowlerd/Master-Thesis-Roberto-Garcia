using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class BcrPgsqlCombatMechanicsMetricsBuilder:ABcrPsqlInsertItemBuilder<Bcr_Psql_Combat_mechanics_metrics,CombatPerPlayerDataCollection>
    {
        public BcrPgsqlCombatMechanicsMetricsBuilder(CombatPerPlayerDataCollection targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_Combat_mechanics_metrics CreateFrom(CombatPerPlayerDataCollection combatPerPlayerData)
        {
            return new Bcr_Psql_Combat_mechanics_metrics()
            {
                block_times_used = combatPerPlayerData.BlockTimesUsed,
                parry_count = combatPerPlayerData.ParryCount,
                time_on_floor = combatPerPlayerData.TimeOnFloor,
                time_on_air = combatPerPlayerData.TimeOnAir,
                down_state_count = combatPerPlayerData.DownStateCount,
                was_defeated = combatPerPlayerData.WasDefeated,
                is_winner_team = combatPerPlayerData.WasTeamVictory,
                
                
                last_position = new float[3]
                    { combatPerPlayerData.LastPosition.x, combatPerPlayerData.LastPosition.y, combatPerPlayerData.LastPosition.z },
                
                bcr_overdrive_times_used = combatPerPlayerData.BcrOVerdriveTimesUsed,
                bcr_overdrive_times_hit = combatPerPlayerData.BcrOVerdriveTimesHit,
                bcr_overdrive_damage_dealt = combatPerPlayerData.BcrOVerdriveDamageDealt,

                gun_times_used = combatPerPlayerData.GunTimesUsed,
                gun_times_hit = combatPerPlayerData.GunTimesHit,
                gun_damage_dealt = combatPerPlayerData.GunDamageDealt,

                melee_times_used = combatPerPlayerData.MeleeTimesUsed,
                melee_times_hit = combatPerPlayerData.MeleeTimesHit,
                melee_damage_dealt = combatPerPlayerData.MeleeDamageDealt,

                bomb_times_used = combatPerPlayerData.BombTimesUsed,
                bomb_times_hit = combatPerPlayerData.BombTimesHit,
                bomb_damage_dealt = combatPerPlayerData.BombDamageDealt,

                pod_times_used = combatPerPlayerData.PodTimesUsed,
                pod_times_hit = combatPerPlayerData.PodTimesHit,
                pod_damage_dealt = combatPerPlayerData.PodDamageDealt
                
                
                
            };
        }
    }
}