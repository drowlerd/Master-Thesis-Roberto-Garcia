using System;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using DataCollection.BcrRabbitMQ;
using UnityEngine;

namespace DataCollection.DataCollectors.Base
{
    public class ADataCollector:MonoBehaviour
    {
        private Bcr_RabbitMQProducer _mqProducer;

        private void Awake()
        {
            _mqProducer = Bcr_RabbitMQProducer.Instance;
            PrimitiveAwake();
        }

        protected virtual void PrimitiveAwake() {}

        protected async void SendMessageToProducer(string message)
        {
            await _mqProducer.Send(message);
        }
        
    }
}