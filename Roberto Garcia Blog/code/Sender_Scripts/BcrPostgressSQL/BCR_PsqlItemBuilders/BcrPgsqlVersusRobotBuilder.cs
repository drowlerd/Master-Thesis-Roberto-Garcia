using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using GameplayEntities.Robot.RobotData;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class BcrPgsqlVersusRobotBuilder : ABcrPsqlVersusRobotInfoBuilder<Bcr_Psql_versus_robot>
    {
        public BcrPgsqlVersusRobotBuilder(RobotInfo targetObject) : base(targetObject)
        {
        }

        protected override Bcr_Psql_versus_robot CreateFrom(RobotInfo robotInfo)
        {
            var pieces = robotInfo.Build;
            var bcr = pieces.Bcr.GetPieceSO();
            var gun = pieces.Gun.GetPieceSO();
            var bomb = pieces.Bomb.GetPieceSO();
            var melee = pieces.Melee.GetPieceSO();
            var pod = pieces.Pod.GetPieceSO();
            var chip = pieces.Chips[0].GetPieceSO();
            return new Bcr_Psql_versus_robot()
            {
                bcr_piece_id = bcr.PieceGuid,
                bcr_piece_version = "0",
                gun_piece_id = gun.PieceGuid,
                gun_piece_version = "0",
                melee_piece_id = melee.PieceGuid,
                melee_piece_version = "0",
                bomb_piece_id = bomb.PieceGuid,
                bomb_piece_version = "0",
                pod_piece_id = pod.PieceGuid,
                pod_piece_version = "0",
                chip_piece_id = chip.PieceGuid,
                chip_piece_version = "0",
            };
        }
    }
}