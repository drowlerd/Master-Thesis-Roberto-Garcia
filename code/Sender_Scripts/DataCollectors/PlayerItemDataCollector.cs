using System;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.DataCollectors.Base;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using UnityEngine;

namespace DataCollection.DataCollectors
{
    public class PlayerItemDataCollector : ADataCollector
    {
        
        I_BCR_Psql_InsertItem _playerItem;


        void Create()
        {
            _playerItem = new Bcr_Psql_player()
            {
                username = "Gamegen 2025",
                registration_date = new DateTimeOffset(2025, 12, 11, 0, 0, 0, TimeSpan.Zero),
                last_login = new DateTimeOffset(2025, 12, 11, 0, 0, 0, TimeSpan.Zero)
            };
        }
    }
}