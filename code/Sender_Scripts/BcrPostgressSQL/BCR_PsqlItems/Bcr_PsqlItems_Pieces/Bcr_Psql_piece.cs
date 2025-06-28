using System;
using System.Collections.Generic;
using System.Linq;
using Customization.Pieces.Base;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using Misc;
using UnityEngine;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlItems_Pieces
{
    public abstract class Bcr_Psql_piece:I_BCR_Psql_InsertItem
    {
        public string id {get;set;}
        public string version {get;set;}
        public string name {get;set;}
        public string description {get;set;}
        public abstract string GetTable();

        protected Bcr_Psql_piece(PieceSO piece)
        {
            id = piece.PieceGuid; //name of piece asset
            version = "0"; //name of piece asset
            name = piece.LocalizedPieceNameKey.GetLocalizedString(); //localized EN name of piece
            description = piece.LocalizedPieceDescriptionKey.GetLocalizedString(); // localized EN description of obj
        }

        public string[] GetColumNames()
        {
            var baseColumnNames= new string[] { "id", "version", "name", "description" };
            return baseColumnNames.Union(GetPieceColumnNames()).ToArray();
        }

        protected abstract IEnumerable<string> GetPieceColumnNames();

        public object[] GetValues()
        {
            var baseValues = new object[] { id , version, name, description };
            return baseValues.Union(GetPieceValues()).ToArray();
        }

        protected abstract IEnumerable<object> GetPieceValues();
    }
}