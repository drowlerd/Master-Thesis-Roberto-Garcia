using System.Collections.Generic;
using System.Linq;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_versus_robot : I_BCR_Psql_InsertItem
    {
        public int id { get; set; }
        public int match_id { get; set; }

        public string gun_piece_id { get; set; }
        public string gun_piece_version { get; set; }

        public string bcr_piece_id { get; set; }
        public string bcr_piece_version { get; set; }

        public string melee_piece_id { get; set; }
        public string melee_piece_version { get; set; }

        public string bomb_piece_id { get; set; }
        public string bomb_piece_version { get; set; }

        public string pod_piece_id { get; set; }
        public string pod_piece_version { get; set; }

        public string chip_piece_id { get; set; }
        public string chip_piece_version { get; set; }

        public string GetTable()
        {
            return "versus_robot";
        }

        // protected abstract IEnumerable<string> GetVersusRobotColumnNames();

        public string[] GetColumNames()
        {
            var baseColumnIds = new string[]
            {
                // "id", 
                "match_id",
                "gun_piece_id",
                "gun_piece_version",
                "bcr_piece_id",
                "bcr_piece_version",
                "melee_piece_id",
                "melee_piece_version",
                "bomb_piece_id",
                "bomb_piece_version",
                "pod_piece_id",
                "pod_piece_version",
                "chip_piece_id",
                "chip_piece_version"
            };
            return  baseColumnIds;
            // if(GetVersusRobotColumnNames().Count()!=0)
            //     baseColumnIds.AddRange(GetVersusRobotColumnNames());
        }

        public object[] GetValues()
        {
            var getBaseValues = new object[] 
            {
                "[fk:match_id]",
                gun_piece_id,
                gun_piece_version,
                bcr_piece_id,
                bcr_piece_version,
                melee_piece_id,
                melee_piece_version,
                bomb_piece_id,
                bomb_piece_version,
                pod_piece_id,
                pod_piece_version,
                chip_piece_id,
                chip_piece_version
            };
            
            return  getBaseValues;
                
        }

    }
}