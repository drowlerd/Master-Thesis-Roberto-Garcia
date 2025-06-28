using System;
using System.Collections.Generic;
using ArenaSceneScripts;
using ArenaSceneScripts.ArenaGameManagers;
using DataCollection.BcrPostgressSQL;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators;
using DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators.Bcr_PgsqlPiecesMetricsDecorators;
using DataCollection.DataCollectors.Base;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using DependencyInjection;
using Game_Scripts;
using GameplayEntities.Robot.RobotData;

namespace DataCollection.DataCollectors
{
    public class MatchDataCollector : ADataCollector
    {
        [Inject] private StageTimeController stageTimeController;
        private I_BCR_Psql_InsertItem[] insertRobotItems;
        private I_BCR_Psql_InsertItem _matchItem;

        protected override void PrimitiveAwake()
        {
            ArenaGameManager.OnStateChanged += HandleStateChanged;
            GenerateMatchData();
        }

        private void OnDestroy()
        {
            ArenaGameManager.OnStateChanged -= HandleStateChanged;
        }

        private void HandleStateChanged(ArenaState newArenaState)
        {
            if (newArenaState != ArenaState.RoundFinished) return;
            try
            {
                var commands = CreateMatchAndRobotsCommand();
                commands.ForEach(SendMessageToProducer);
            }
            catch
            {
                // ignored
            }
        }

        private void GenerateRobotsItems()
        {
            var robotsInfo = Game.Instance.TemporalDataManager.BattleData.PlayersInfo;

            insertRobotItems = new I_BCR_Psql_InsertItem[robotsInfo.Count];
            for (int i = 0; i < robotsInfo.Count; i++)
            {
                var robotInfo = robotsInfo[i];
                insertRobotItems[i] = GetInsertItemForRobotInfo(robotInfo);
            }
        }

        private I_BCR_Psql_InsertItem GetInsertItemForRobotInfo(RobotInfo robotInfo)
        {
            return new BcrPgsqlVersusRobotBuilder(robotInfo);
            return robotInfo.IsAI
                ? new BcrPsqlVersusRobotAIBuilder(robotInfo)
                : new BcrPsqlVersusRobotHumenBuilder(robotInfo);
        }

        private void GenerateMatchData()
        {
            
            var mapData = Game.Instance.TemporalDataManager.TemporalMapData;
            _matchItem = new Bcr_Psql_match()
            {
                arena_id = mapData.selectedMap.GetMapVariationByIndex(mapData.variationIndex).name,
                duration = TimeSpan.FromSeconds(0),
            };

            SendMessageToProducer(Bcr_PostgreSQLExtensions.AddReaderKey("match_id") +
                                  _matchItem.CreateInsertCommandAsText() + " RETURNING id");
        }


        private string CreateWithSqlCommand(string insertCommand, string referenceName, string[] returningValues = null)
        {
            var command = $"WITH {referenceName} AS (\n" +
                          insertCommand;
            if (returningValues is { Length: > 0 })
            {
                command += $" RETURNING {string.Join(",", returningValues)}";
            }

            command += "\n)";
            return command;
        }

        private string GameplayMetricsCombat(CombatPerPlayerDataCollection combatData)
        {
            string CreateGameplayMetricWithCommand(I_BCR_Psql_InsertItem item, string metricsReferenceName,
                bool includeComma = true)
            {
                var command = $" {metricsReferenceName} AS (\n"
                              + item.CreateInsertCommandAsText() + " RETURNING id \n)";
                command += includeComma ? ",\n" : "\n";
                return command;
            }

            var command = "";
            command += CreateGameplayMetricWithCommand(new BcrPgsqlBcrPieceGameplayBuilder(combatData),
                "bcr_gameplay_metric");

            command += CreateGameplayMetricWithCommand(new BcrPgsqlGunPieceGameplayBuilder(combatData),
                "gun_gameplay_metric");

            command += CreateGameplayMetricWithCommand(new BcrPgsqlMeleePieceGameplayBuilder(combatData),
                "melee_gameplay_metric");

            command += CreateGameplayMetricWithCommand(new BcrPgsqlBombPieceGameplayBuilder(combatData),
                "bomb_gameplay_metric");

            command += CreateGameplayMetricWithCommand(new BcrPgsqlPodPieceGameplayBuilder(combatData),
                "pod_gameplay_metric", false);

            return command;
        }

        private List<string> CreateMatchAndRobotsCommand()
        {
            GenerateRobotsItems();
            GenerateMatchData();
            List<string> commands = new List<string>();

            int roundIndex = Game.Instance.TemporalDataManager.BattleData.currentRound;
            var roundData = DataCollectionReference.Instance.GetRoundData(roundIndex - 1);

            var robotInfos = Game.Instance.TemporalDataManager.BattleData.PlayersInfo;

            foreach (var robotInfo in robotInfos)
            {
                var playerId = $"robot{robotInfo.standardShortID.Replace(" ", "")}";
                var newCommand =
                    CreateWithSqlCommand(GetInsertItemForRobotInfo(robotInfo).CreateInsertCommandAsText(),
                        $"{playerId}", new string[] { "id" }) + "\n";

                var combatData = roundData.PlayerRoundsData.Find(
                    p => p.PlayerID.Equals(robotInfo.standardShortID)).PlayerCombatData;

                I_BCR_Psql_InsertItem combatMechanicsItem = new BcrPgsqlCombatMechanicsMetricsBuilder(combatData);
                newCommand += $" {combatMechanicsItem.InsertClauseString()}\n" +
                              " SELECT " +
                              $"{playerId}.id,{combatMechanicsItem.GetValuesString(false)}" +
                              $" FROM {playerId};\n";

                commands.Add(newCommand);
            }

            var updateMatchDuration = $"UPDATE {_matchItem.GetTable()} " +
                                      $"SET duration = {Bcr_PostgreSQLExtensions.FormatSqlValue(TimeSpan.FromSeconds(stageTimeController.ElapsedTime))}\n" +
                                      $"WHERE id = '[fk:match_id]';";

            commands.Add(updateMatchDuration);

            return commands;
        }
    }
}