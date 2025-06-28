using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using GameplayEntities.Robot.RobotData;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class BcrPsqlVersusRobotHumenBuilder:ABcrPsqlVersusRobotInfoBuilder<Bcr_Psql_versus_human_robot>
    {
        public BcrPsqlVersusRobotHumenBuilder(RobotInfo targetObject) : base(targetObject)
        {
        }
        

        protected override Bcr_Psql_versus_human_robot CreateFrom(RobotInfo robotInfo)
        {
            return new Bcr_Psql_versus_human_robot()
            {
                device_scheme = robotInfo.TemporalInputData.ControlScheme
            };
        }
    }
}