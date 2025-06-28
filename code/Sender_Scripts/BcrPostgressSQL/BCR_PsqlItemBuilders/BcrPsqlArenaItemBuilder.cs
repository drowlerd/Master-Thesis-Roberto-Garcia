using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using Maps.Variations;
using Maps.Variations.Base;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class BcrPsqlArenaItemBuilder:ABcrPsqlInsertItemBuilder<Bcr_Psql_arena,MapVariation>

    {
        public BcrPsqlArenaItemBuilder(MapVariation targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_arena CreateFrom(MapVariation robotInfo)
        {
            return new Bcr_Psql_arena()
            {
                id = robotInfo.gameObject.name,
                name = robotInfo.gameObject.name,
            };
        }
    }
}