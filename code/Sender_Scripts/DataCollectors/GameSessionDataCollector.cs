using System;
using System.Collections;
using DataCollection.BcrPostgressSQL;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.DataCollectors.Base;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using UnityEngine;

namespace DataCollection.DataCollectors
{
    public class GameSessionDataCollector : ADataCollector
    {
        I_BCR_Psql_InsertItem _gameSessionItem;
        private float _elaspedDuration = 0f;
        Coroutine _coroutine;
        private void Start()
        {
            StartElapsedDurationCoroutine();
            SendMessageToProducer($"{Bcr_PostgreSQLExtensions.AddReaderKey("game_session_id")}" + CreateSesionItem().CreateInsertCommandAsText() + " RETURNING id");
        }

        private void StartElapsedDurationCoroutine()
        {
            if(_coroutine==null)
            {
                StopCoroutine(DurationCoroutine());
                _coroutine = null;
            }            
            _coroutine = StartCoroutine(DurationCoroutine());
        }

        private void StopCoroutine()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        IEnumerator DurationCoroutine()
        {
            while (true)
            {
                _elaspedDuration+=Time.deltaTime;
                yield return null;
            }
            
        }

        I_BCR_Psql_InsertItem CreateSesionItem()
        {
            _gameSessionItem = new Bcr_Psql_game_session()
            {
                player_id = 1, //Haardcoded para la Gamegen
                session_start = DateTimeOffset.UtcNow,
                // session_end = ,
                duration = TimeSpan.FromSeconds(0)
            };
            return _gameSessionItem;
        }


        void UpdateDuration()
        {
            var elapsedDurationString = Bcr_PostgreSQLExtensions.FormatSqlValue(_elaspedDuration);
            var message =$"UPDATE game_session SET duration = {elapsedDurationString} WHERE ID = PK:GAME_SESSION";
        }
    }
}