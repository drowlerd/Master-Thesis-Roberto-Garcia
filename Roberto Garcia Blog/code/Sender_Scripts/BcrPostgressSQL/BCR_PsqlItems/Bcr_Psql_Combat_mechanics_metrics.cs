using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_Combat_mechanics_metrics:I_BCR_Psql_InsertItem
    {
        public int versus_robot_id { get; set; }
        public int block_times_used { get; set; }
        public int parry_count { get; set; }
        public float time_on_floor { get; set; }
        public float time_on_air { get; set; }
        public int down_state_count { get; set; }
        public bool was_defeated { get; set; }
        public bool is_winner_team { get; set; }
        public float[] last_position { get; set; }
        
        // Nueva estructura con las métricas desglosadas
        public int bcr_overdrive_times_used { get; set; }
        public int bcr_overdrive_times_hit { get; set; }
        public int bcr_overdrive_damage_dealt { get; set; }

        public int gun_times_used { get; set; }
        public int gun_times_hit { get; set; }
        public int gun_damage_dealt { get; set; }

        public int melee_times_used { get; set; }
        public int melee_times_hit { get; set; }
        public int melee_damage_dealt { get; set; }

        public int bomb_times_used { get; set; }
        public int bomb_times_hit { get; set; }
        public int bomb_damage_dealt { get; set; }

        public int pod_times_used { get; set; }
        public int pod_times_hit { get; set; }
        public int pod_damage_dealt { get; set; }

        public string GetTable()
        {
            return "combat_mechanics_metrics";
        }

        public string[] GetColumNames()
        {
            return new[]
            {
                nameof(versus_robot_id),
                nameof(block_times_used),
                nameof(parry_count),
                nameof(time_on_floor),
                nameof(time_on_air),
                nameof(down_state_count),
                nameof(was_defeated),
                nameof(is_winner_team),
                nameof(last_position),
                
                // Nuevos campos de las métricas
                nameof(bcr_overdrive_times_used),
                nameof(bcr_overdrive_times_hit),
                nameof(bcr_overdrive_damage_dealt),

                nameof(gun_times_used),
                nameof(gun_times_hit),
                nameof(gun_damage_dealt),

                nameof(melee_times_used),
                nameof(melee_times_hit),
                nameof(melee_damage_dealt),

                nameof(bomb_times_used),
                nameof(bomb_times_hit),
                nameof(bomb_damage_dealt),

                nameof(pod_times_used),
                nameof(pod_times_hit),
                nameof(pod_damage_dealt)
            };
        }

        public object[] GetValues()
        {
            return new object[]
            {
                // versus_robot_id,
                block_times_used,
                parry_count,
                time_on_floor,
                time_on_air,
                down_state_count,
                was_defeated,
                is_winner_team,
                last_position,
                
                // Nuevos valores para las métricas
                bcr_overdrive_times_used,
                bcr_overdrive_times_hit,
                bcr_overdrive_damage_dealt,

                gun_times_used,
                gun_times_hit,
                gun_damage_dealt,

                melee_times_used,
                melee_times_hit,
                melee_damage_dealt,

                bomb_times_used,
                bomb_times_hit,
                bomb_damage_dealt,

                pod_times_used,
                pod_times_hit,
                pod_damage_dealt
            };
        }
    }
}
