using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;
    
    private Collider2D collectableCol2D;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        //Genera una ID aleatoria a traves del inspector
        id = System.Guid.NewGuid().ToString();
    }
    
    private bool isCollected;

    private void Awake()
    {
        collectableCol2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            collectableCol2D.enabled = false;
            this.gameObject.SetActive(false);
            Debug.Log("collisions");
            AddCollectable();
        }
    }

    private void AddCollectable()
    {
        CollectableManager collectableManager = FindObjectOfType<CollectableManager>();
        collectableManager.AddCollectable();
    }

    public void LoadData(GameData data)
    {
        data.starsCollected.TryGetValue(id, out isCollected);
        if (isCollected)
        {
            //spriteRenderer.enabled = false;
            //collectableCol2D.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.starsCollected.ContainsKey(id))
        {
            data.starsCollected.Remove(id);
        }
        data.starsCollected.Add(id, isCollected);
    }
}
