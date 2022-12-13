using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaterTouchingTest : MonoBehaviour, IAffectedByWaterDrop
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private UnityEvent unityEvent;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoStuffsWhenWaterTouch()
    {
        spriteRenderer.color = Color.gray;
        unityEvent.Invoke();
    }

    public void DoStuffsWhenWaterDontTouch()
    {
        Debug.Log("Test");
    }
}
