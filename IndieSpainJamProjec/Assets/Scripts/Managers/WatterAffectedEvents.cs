using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WatterAffectedEvents : MonoBehaviour, IAffectedByWaterDrop
{
    [SerializeField] private UnityEvent eventToInvoke;
    
    public void DoStuffsWhenWaterTouch()
    {
        eventToInvoke.Invoke();
    }

    public void DoStuffsWhenWaterDontTouch()
    {
        Debug.Log("test");
    }
}
