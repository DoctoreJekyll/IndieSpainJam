using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private bool isCollectableEnable;
    private void Start()
    {
        isCollectableEnable = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            AddCollectable();
            isCollectableEnable = false;
            this.gameObject.SetActive(isCollectableEnable);
        }
    }

    private void AddCollectable()
    {
        CollectableManager collectableManager = FindObjectOfType<CollectableManager>();
        collectableManager.AddCollectable();
    }
    
}
