using System;
using DataCollection.BcrPostgressSQL;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators;
using DataCollection.DataCollectors.Base;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using DataManagment.TemporalData;
using DependencyInjection;
using Maps.MapSelectionScreen;
using UnityEngine;

namespace DataCollection.DataCollectors
{
    public class MatchSetDataCollector :ADataCollector
    {
        [Inject] private MapSelectionScreenDataHandler mapSelectionScreenDataHandler;
        
        private I_BCR_Psql_InsertItem _matchSetItem;


        protected override void PrimitiveAwake()
        {
            base.PrimitiveAwake();
            if(mapSelectionScreenDataHandler)
                mapSelectionScreenDataHandler.OnMatchSetDataSet += HandleMatchSetStarted;
        }

        private void HandleMatchSetStarted(BattleData battleDataSet)
        {
            try
            {
                _matchSetItem = new BcrPsqlMatchSetItemBuilder(battleDataSet);
                SendMessageToProducer(Bcr_PostgreSQLExtensions.AddReaderKey("match_set_id") +
                                      _matchSetItem.CreateInsertCommandAsText() + " RETURNING id;");

            }
            catch
            {
                // ignored
            }
        }

        private void OnDestroy()
        {
            if(mapSelectionScreenDataHandler)
                mapSelectionScreenDataHandler.OnMatchSetDataSet -= HandleMatchSetStarted;
        }
 
        [ContextMenu("Test")]
        void TestSendMatchSet()
        {
            I_BCR_Psql_InsertItem testItem = new Bcr_Psql_match_set()
            {
                all_matches_completed = false,
                rematch_pressed = false,
                set_type = 1,
                match_duration_selected = -1
            };
            SendMessageToProducer(Bcr_PostgreSQLExtensions.AddReaderKey("match_set_id")+testItem.CreateInsertCommandAsText() + " RETURNING id;");
        }
    }
}