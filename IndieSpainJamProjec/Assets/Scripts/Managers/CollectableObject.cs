using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            AddCollectable();
            this.gameObject.SetActive(false);
        }
    }

    private void AddCollectable()
    {
        CollectableManager collectableManager = FindObjectOfType<CollectableManager>();
        collectableManager.AddCollectable();
    }
}
