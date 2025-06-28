using System.Linq;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using GameplayEntities.Robot.Controller;
using GameplayEntities.Robot.RobotData;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public abstract class ABcrPsqlVersusRobotInfoBuilder<T>:ABcrPsqlInsertItemBuilder<T,RobotInfo> where T: I_BCR_Psql_InsertItem
    {
        protected ABcrPsqlVersusRobotInfoBuilder(RobotInfo targetObject) : base(targetObject)
        {
        }

        
    }
}