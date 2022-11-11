using System.Collections;
using UnityEngine;

public class HiddenArea : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float alphaValue = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.color =
            new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alphaValue);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetHiddenAlphaToZero());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetHiddenAlphaToOne());
        }
    }

    IEnumerator SetHiddenAlphaToZero()
    {
        while (spriteRenderer.color.a > 0)
        {
            alphaValue -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    IEnumerator SetHiddenAlphaToOne()
    {
        while (spriteRenderer.color.a < 1)
        {
            alphaValue += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
