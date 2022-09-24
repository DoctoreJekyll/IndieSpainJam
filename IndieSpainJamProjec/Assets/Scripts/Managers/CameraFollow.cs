using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject target;
    private Vector3 targetPos;

    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    public float smooth;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Lakitu");
        transform.position = new Vector3(target.transform.position.x, transform.position.y, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -10f);

        if (target.transform.localScale.x == 1)
        {
            targetPos = new Vector3(targetPos.x + offsetX, targetPos.y + offsetY, -10f);
        }

        if (target.transform.localScale.x == -1)
        {
            targetPos = new Vector3(targetPos.x - offsetX, targetPos.y + offsetY, -10f);
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);

    }
}
