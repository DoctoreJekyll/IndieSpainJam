using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubbleTemp : MonoBehaviour
{
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 add = new Vector3(0.85f, 0.85f, 0f);
        Vector3 newTransform = playerTransform.position + add;

        if (playerTransform != null)
        {
            transform.position = newTransform;
        }
    }
}
