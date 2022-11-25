using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        //Genera una ID aleatoria a traves del inspector
        id = System.Guid.NewGuid().ToString();
    }
    
    private bool isCollected;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            AddCollectable();
            isCollected = true;
            this.gameObject.SetActive(false);
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
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.starsCollected.ContainsKey(id))
        {
            data.starsCollected.Remove(id);
        }
        data.starsCollected.Add(id, isCollected);
    }
}
