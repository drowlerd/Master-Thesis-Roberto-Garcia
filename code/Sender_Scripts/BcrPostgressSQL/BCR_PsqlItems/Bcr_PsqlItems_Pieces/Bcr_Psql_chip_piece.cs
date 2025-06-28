using System.Collections.Generic;
using Customization.Pieces.Base;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlItems_Pieces
{
    public class Bcr_Psql_chip_piece:Bcr_Psql_piece
    {
        public Bcr_Psql_chip_piece(PieceSO piece) : base(piece)
        {
        }

        public override string GetTable()
        {
            return "chip_piece";
        }

        protected override IEnumerable<string> GetPieceColumnNames()
        {
            return System.ArraySegment<string>.Empty;
        }

        protected override IEnumerable<object> GetPieceValues()
        {
            return System.ArraySegment<object>.Empty;
        }
    }
}