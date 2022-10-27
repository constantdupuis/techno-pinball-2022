using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickEventController : MonoBehaviour
{
    public UnityEvent<int> OnClick;

    // Start is called before the first frame update
    void Start()
    {
        OnClick.AddListener(DebugClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick.Invoke(Time.frameCount); 
        }
    }

    private void DebugClick(int frameCount)
    {
        Debug.Log($"Clicked! {frameCount}");
    }
}
