using System.Collections.Generic;
using DataCollection.BcrPostgressSQL.BCR_PsqlItemsDecorators;
using DataCollection.BcrRabbitMQ;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using Game_Scripts;
using Maps.Variations.MapsScriptableObjects.Base;
using Misc;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DataCollection.BcrPostgressSQL.Bcr_PsqlStaticItemsUploader
{
    public class Bcr_PgsqlArenaUploader:MonoBehaviour
    {
        [SerializeField] Bcr_RabbitMQProducer rabbitMQProducer;
        private Dictionary<SOMap, AsyncOperationHandle<SOMap>> _availableMaps;
        private void LoadMapsFromAddressables()
        {
            var mapsSOLabel = "Gameplay/MapSO";
            var mapsLocations = Addressables.LoadResourceLocationsAsync(mapsSOLabel, typeof(SOMap)).WaitForCompletion();
            _availableMaps = new Dictionary<SOMap, AsyncOperationHandle<SOMap>>();
            foreach (var resourceLocation in mapsLocations)
            {
                var handle = Addressables.LoadAssetAsync<SOMap>(resourceLocation);
                var map = handle.WaitForCompletion();

#if UNITY_EDITOR
                var mapGuid = map.GetAddressableAssetGuid();
                if (mapGuid != -1)
                {
                    _availableMaps.Add(map,
                        handle);
                }
                else
                {
                    Addressables.Release(handle);
                }
#else
                 _availableMaps.Add(map,
                        handle);
#endif
            }
        }

        [ContextMenu("Upload maps")]
        async void UploadMaps()
        {
            LoadMapsFromAddressables();
            List<I_BCR_Psql_InsertItem> insertItems = new List<I_BCR_Psql_InsertItem>();
            foreach (var value in _availableMaps.Values)
            {
                for (int i = 0; i < value.Result.VariationsCount; i++)
                {
                    var variation = value.Result.GetMapVariationByIndex(i);
                    insertItems.Add(new Bcr_PsqlArenaItemDecorator(variation));

                }
            }

            var command = "";
            foreach (var bcrPsqlInsertItem in insertItems)
            {
                command += bcrPsqlInsertItem.CreateInsertCommandAsText() + ";\n";
                await rabbitMQProducer.Send(bcrPsqlInsertItem.CreateInsertCommandAsText());
            }
            
            Dbg.Log(command);
        }
    }
}