using System;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_player:I_BCR_Psql_InsertItem
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTimeOffset registration_date { get; set; } 
        public DateTimeOffset last_login { get; set; } 
        public string GetTable()
        {
            return "player";
        }

        public string[] GetColumNames()
        {
            return new string[]
            {
                // "id", 
                "username", 
                "registration_date", 
                "last_login"
            };
        }

        public object[] GetValues()
        {
            return new object[]
            {
                // id,
                username,
                registration_date,
                last_login
            };
        }
    }
}