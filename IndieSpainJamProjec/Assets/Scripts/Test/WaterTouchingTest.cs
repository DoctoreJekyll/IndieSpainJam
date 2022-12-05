using UnityEngine;

public class WaterTouchingTest : MonoBehaviour, IAffectedByWaterDrop
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DoStuffsWhenWaterTouch()
    {
        spriteRenderer.color = Color.gray;
    }

    public void DoStuffsWhenWaterDontTouch()
    {
        Debug.Log("Test");
    }
}
