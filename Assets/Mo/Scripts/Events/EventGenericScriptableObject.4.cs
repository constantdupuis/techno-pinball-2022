using UnityEngine;
using UnityEngine.Events;

namespace Mo.Events
{
    public abstract class EventGenericScriptableObject<T0, T1, T2, T3> : ScriptableObject
    {
        private UnityEvent<T0, T1, T2, T3> OnActivate;

        #region Helpers
        public void AddListener(UnityAction<T0, T1, T2, T3> call)
        {
            OnActivate.AddListener(call);
        }

        public void Invoke(T0 data0, T1 data1, T2 data2, T3 data3)
        {
            OnActivate.Invoke(data0, data1, data2, data3);
        }

        public void RemoveAllListener()
        {
            OnActivate.RemoveAllListeners();
        }

        public void RemoveListener(UnityAction<T0, T1, T2, T3> call)
        {
            OnActivate.RemoveListener(call);
        }
        #endregion
    }
}
