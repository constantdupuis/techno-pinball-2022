using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivation : MonoBehaviour
{
    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }
    
    public void TurnOnOff(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
