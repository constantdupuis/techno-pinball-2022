
using UnityEngine;
using UnityEngine.Events;

namespace Mo.Events
{
    public abstract class EventGenericScriptableObject<T0> : ScriptableObject
    {
        private UnityEvent<T0> OnActivate;

        #region Helpers
        public void AddListener(UnityAction<T0> call)
        {
            OnActivate.AddListener(call);
        }

        public void Invoke(T0 data)
        {
            OnActivate.Invoke(data);
        }

        public void RemoveAllListener()
        {
            OnActivate.RemoveAllListeners();
        }

        public void RemoveListener(UnityAction<T0> call)
        {
            OnActivate.RemoveListener(call);
        }
        #endregion
    }
}
