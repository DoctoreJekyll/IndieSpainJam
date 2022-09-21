using System.Collections;
using UnityEngine;

public class DoorClose : MonoBehaviour
{

    public void CloseDoorAnim()
    {
        StartCoroutine(CloseDoorCoroutine());
    }

    private IEnumerator CloseDoorCoroutine()
    {
        Vector3 actualScale = transform.localScale;
        Vector3 newScale = Vector3.zero;
        float speed = 2f;

        while (actualScale.magnitude >= newScale.magnitude)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
        Destroy(this.gameObject);
        
    }

    
    
}
