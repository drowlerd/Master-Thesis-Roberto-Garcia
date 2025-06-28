using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using DataManagment.TemporalData;
using UnityEngine;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class BcrPsqlMatchSetItemBuilder: ABcrPsqlInsertItemBuilder<Bcr_Psql_match_set,BattleData>
    {
        public BcrPsqlMatchSetItemBuilder(BattleData targetObject) : base(targetObject)
        {}


        protected override Bcr_Psql_match_set CreateFrom(BattleData robotInfo)
        {
            return  new Bcr_Psql_match_set()
            {
                set_type = robotInfo.rounds, 
                match_duration_selected = (int)robotInfo.matchTime,
                all_matches_completed = false,
                rematch_pressed = false, 
            };
        }
    }
}