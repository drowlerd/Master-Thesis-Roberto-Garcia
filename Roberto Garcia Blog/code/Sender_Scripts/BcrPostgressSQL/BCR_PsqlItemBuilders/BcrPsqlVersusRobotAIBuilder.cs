using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using GameplayEntities.Robot.RobotData;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class BcrPsqlVersusRobotAIBuilder:ABcrPsqlVersusRobotInfoBuilder<Bcr_Psql_versus_AI_robot>
    {
        public BcrPsqlVersusRobotAIBuilder(RobotInfo targetObject) : base(targetObject)
        {
        }
        

        protected override Bcr_Psql_versus_AI_robot CreateFrom(RobotInfo robotInfo)
        {
            var aiInfo = robotInfo.aiInfo;
            return new Bcr_Psql_versus_AI_robot()
            {
                AI_difficulty = (int)aiInfo.aiLevel,
                bottom_threshold = aiInfo.bottomThreshold,
                top_threshold = aiInfo.topThreshold,
            };
        }
    }
}