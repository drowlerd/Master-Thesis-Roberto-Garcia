using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Customization.Pieces.Base;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.PostgreSQLEditors
{
    public class PiecesPostgreSQLTools : EditorWindow
    {
        private const string PiecesParentPath = "Assets/ScriptableObjs/Pieces";
        private bool _duplicatedPiecesCheked = false;
        private string _stringDuplicatedPiecesResult;

        [MenuItem("PostgreSQL/Pieces Postgre SQL tools")]
        private static void ShowWindow()
        {
            var window = GetWindow<PiecesPostgreSQLTools>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }

        private void CreateGUI()
        {
            _duplicatedPiecesCheked = false;
            _stringDuplicatedPiecesResult = "";
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Create pieces GUID where empty"))
            {
                CreateAllPiecesGuidWhereEmpty();
            }

            if (GUILayout.Button("Check duplicated pieces GUIDs"))
            {
                CheckPiecesDuplicatedGUIDS();
            }

            if (_duplicatedPiecesCheked)
                GUILayout.Label(_stringDuplicatedPiecesResult);
        }

        private void CheckPiecesDuplicatedGUIDS()
        {
            HashSet<PieceSO> piecesDuplicated = new HashSet<PieceSO>();

            bool CheckPieceGuidDuplicated(PieceSO p1, PieceSO p2)
            {
                var result = p1!=p2 && p1.PieceGuid.Equals(p2.PieceGuid);

                if (result && piecesDuplicated.Add(p1))
                {
                    _stringDuplicatedPiecesResult += $"{p1.name} duplicated with guid: {p1.PieceGuid}\n" +
                                                     $"piece {p2.name} same guid: {p2.PieceGuid}\n \n";
                }

                return result;
            }

            var pieceSOs = Editor.BcrEditorTools.GetPiecesInPath<PieceSO>(PiecesParentPath);
            foreach (var pieceSO in pieceSOs)
            {
                if (!pieceSOs.Any(p => CheckPieceGuidDuplicated(p, pieceSO))) continue;
            }

            _duplicatedPiecesCheked = true;
            if (!piecesDuplicated.Any())
            {
                _stringDuplicatedPiecesResult = "No Guids duplicated :)";
            }
        }

        private void CreateAllPiecesGuidWhereEmpty()
        {
            var pieceSOs = Editor.BcrEditorTools.GetPiecesInPath<PieceSO>(PiecesParentPath);
            foreach (var pieceSo in pieceSOs)
            {
                if (pieceSo.RegenerateGuid())
                    UnityEditor.EditorUtility.SetDirty(pieceSo);
            }

            UnityEditor.AssetDatabase.SaveAssets();
        }
    }
}