using System;
using System.Collections.Generic;
using Customization.Pieces.Base;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlItems_Pieces
{
    public class Bcr_Psql_bcr_piece:Bcr_Psql_piece
    {
        public Bcr_Psql_bcr_piece(PieceSO piece) : base(piece)
        {}

        public override string GetTable()
        {
            return "bcr_piece";
        }

        protected override IEnumerable<string> GetPieceColumnNames()
        {
            return ArraySegment<string>.Empty;
        }

        protected override IEnumerable<object> GetPieceValues()
        {
            return ArraySegment<object>.Empty;
        }
    }
}