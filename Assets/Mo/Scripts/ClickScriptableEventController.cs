using Mo.Events;
using UnityEngine;

public class ClickScriptableEventController : MonoBehaviour
{
    public EventScriptableObject clickEvent;

    private void Awake()
    {
        clickEvent.AddListener(DebugClick);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickEvent.Invoke();
        }
    }

    void DebugClick()
    {
        Debug.Log("ClickScriptableEventController::DebugClick - Clicked!");
    }
}
