using UnityEngine;
using UnityEngine.Events;

namespace Mo.Events
{
    [CreateAssetMenu(fileName = "ScriptableEvent", menuName = "Mo/Events/Void Event", order = 1)]
    public class EventScriptableObject : ScriptableObject
    {
        private UnityEvent OnActivate;

        #region Helpers
        public void AddListener(UnityAction call)
        {
            OnActivate.AddListener(call);
        }

        public void RemoveListener(UnityAction call)
        {
            OnActivate.RemoveListener(call);
        }

        public void RemoveAllListener()
        {
            OnActivate.RemoveAllListeners();
        }

        public void Invoke()
        {
            OnActivate.Invoke();
        }
        #endregion
    }
}


