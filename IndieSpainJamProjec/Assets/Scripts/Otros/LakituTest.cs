using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakituTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = GameObject.FindWithTag("Player").transform;

        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }
}