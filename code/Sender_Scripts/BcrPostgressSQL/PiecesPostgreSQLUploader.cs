using System;
using System.Threading.Tasks;
using Customization.Pieces.Base;
using DataCollection.Editor.PostgreSQL;
using GameplayEntities.Robot.RobotCustomization;
using Npgsql;
using UnityEditor;
using UnityEngine;

namespace DataCollection.BcrPostgressSQL
{
    public class PiecesPostgreSqlUploader : EditorWindow
    {
        private PostgreSqlConnection connection;
        private PieceSO piece;
        
        [MenuItem("PostgreSQL/PieceUploader")]
        private static void ShowWindow()
        {
            var window = GetWindow<PiecesPostgreSqlUploader>();
            window.titleContent = new GUIContent("Pieces posgreSQL Uploader");
            window.Show();
        }

        private async void OnGUI()
        {
            piece = EditorGUILayout.ObjectField("piece", piece, typeof(PieceSO), true) as PieceSO;
            if (GUILayout.Button("Upload"))
            {
              //Obtener secretos a través de YAML
                
               await Upload(connection, piece);
            }
        }

        public async Task Upload(PostgreSqlConnection connection, PieceSO piece)
        {

            var pieceGuid = piece.PieceGuid;
            var pieceName = piece.name;
            var pieceDescription = "";
                    
            await using var dataSource = NpgsqlDataSource.Create(connection.ToNpgsqlConnectionStringBuilder());
            await using (var cmd = dataSource.CreateCommand(
                             $"INSERT INTO {GetPieceTable(piece.PieceType)} (piece_ID,piece_name,description) VALUES (@piece_ID,@pieceName,@pieceDescription)"))
            {
                
                cmd.Parameters.AddWithValue("piece_ID", pieceGuid);
                cmd.Parameters.AddWithValue("pieceName", pieceName);
                cmd.Parameters.AddWithValue("pieceDescription", pieceDescription);

                var query=cmd.ExecuteNonQueryAsync();
                await query;
            }
        }

        private string GetPieceTable(PieceType pieceType)
        {
            return pieceType switch
            {
                PieceType.BC => "bcr_piece",
                PieceType.Gun => "gun_piece",
                PieceType.Bomb => "bomb_piece",
                PieceType.Pod => "pod_piece",
                PieceType.Melee => "melee_piece",
                PieceType.Chip => "chip_piece",
                _ => throw new ArgumentOutOfRangeException(nameof(pieceType), pieceType, null)
            };
        }
    }
}