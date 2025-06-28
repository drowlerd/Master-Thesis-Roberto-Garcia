using System;
using System.Collections.Generic;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems.Bcr_PsqlItems_Pieces;
using DataCollection.BcrRabbitMQ;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using Game_Scripts;
using GameplayEntities.Robot.RobotCustomization;
using UnityEngine;

namespace DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators
{
    public class Bcr_PsqlPieceItemDecorator : MonoBehaviour
    {
        [SerializeField] Bcr_RabbitMQProducer rabbitMQProducer;

        [ContextMenu("Send to database")]
        async void SendToDataBase()
        {
            PiecesSOManager.LoadPieces();

            List<I_BCR_Psql_InsertItem> insertPieceItem = new List<I_BCR_Psql_InsertItem>();
            foreach (var keyValuePair in PiecesSOManager.PiecesMap)
            {
                var pieceType = keyValuePair.Key;
                var pieces = keyValuePair.Value;
                switch (pieceType)
                {
                    case PieceType.BC:
                        Array.ForEach(pieces, (piece) => insertPieceItem.Add(new Bcr_Psql_bcr_piece(piece)));

                        break;
                    case PieceType.Gun:
                        Array.ForEach(pieces, (piece) => insertPieceItem.Add(new Bcr_Psql_gun_piece(piece)));
                        break;
                    case PieceType.Bomb:
                        Array.ForEach(pieces, (piece) => insertPieceItem.Add(new Bcr_Psql_bomb_piece(piece)));
                        break;
                    case PieceType.Pod:
                        Array.ForEach(pieces, (piece) => insertPieceItem.Add(new Bcr_Psql_pod_piece(piece)));
                        break;
                    case PieceType.Melee:
                        Array.ForEach(pieces, (piece) => insertPieceItem.Add(new Bcr_Psql_melee_piece(piece)));
                        break;
                    case PieceType.Chip:
                        Array.ForEach(pieces, (piece) => insertPieceItem.Add(new Bcr_Psql_chip_piece(piece)));
                        break;
                    // case PieceType.Mod:
                    // Array.ForEach(pieces, (piece) =>insertPieceItem.Add(new Bcr_Psql_mod_piece(piece)));
                    // break;
                    // case PieceType.NONE:
                        // break;
                    // default:
                        // throw new ArgumentOutOfRangeException();
                }
            }
            var command = "";
            foreach (var bcrPsqlInsertItem in insertPieceItem)
            {
                command += bcrPsqlInsertItem.CreateInsertCommandAsText() + ";\n";
                await rabbitMQProducer.Send(bcrPsqlInsertItem.CreateInsertCommandAsText());
            }
            Dbg.Log(command);
        }
    }
}