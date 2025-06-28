using System.Collections.Generic;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_versus_human_robot : I_BCR_Psql_InsertItem
    {
        public int versus_robot_id;
        public string device_scheme { get; set; }

        public string GetTable()
        {
            return "versus_human_robot";
        }

        public string[] GetColumNames()
        {
            return new[] { nameof(versus_robot_id),nameof(device_scheme) };
        }

        public object[] GetValues()
        {
            return new object[] { device_scheme };
        }

    }
}