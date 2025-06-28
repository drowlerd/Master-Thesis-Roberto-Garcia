using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems
{
    public class Bcr_Psql_arena: I_BCR_Psql_InsertItem
    {
        public string id {get; set;}
        public string name {get; set;}
        public string description {get; set;}
        public string GetTable()
        {
            return "arena";
        }

        public string[] GetColumNames()
        {
            // return new string[] { "id", "name", "description" };
            return new string[] { 
                "id",
                "name",
                // "description"
            };
        }

        public object[] GetValues()
        {
            return new object[]
            {
                id,
                name,
                // description
            };
        }
    }
}