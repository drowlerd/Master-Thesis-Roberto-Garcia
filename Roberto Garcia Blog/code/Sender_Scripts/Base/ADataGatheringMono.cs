using System;
using UnityEngine;

namespace DataCollection.Base
{
    public abstract class ADataGatheringMono : MonoBehaviour
    {
        private void Awake()
        {
            try
            {
                AddEventsAwake();
            }
            catch (Exception)
            {
            }
        }

        private void Start()
        {
            try
            {
                AddEventsStart();
            }
            catch (Exception)
            {
            }
        }

        private void OnDestroy()
        {
            try
            {
                RemoveEvents();
            }
            catch (Exception e)
            {
            }
        }


        protected abstract void AddEventsStart();
        protected abstract void AddEventsAwake();
        protected abstract void RemoveEvents();
    }
}