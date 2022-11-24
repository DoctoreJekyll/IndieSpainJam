using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour, ISaveable
{
    private CollectableObject[] collects;

    [SerializeField] private TMP_Text totalCollectableText;
    [SerializeField] private TMP_Text actualCollectableText;

    [SerializeField] private float totalCollectable;
    [SerializeField] private float actualCollectable;

    [HideInInspector] public int collectableTaken;

    private void Awake()
    {
        collects = FindObjectsOfType<CollectableObject>();
        
        LoadJsonData(this);
        Debug.Log("Al cargar " +  collectableTaken);
    }

    private void Start()
    {
        totalCollectable = collects.Length;
        
        totalCollectableText.text = totalCollectable.ToString();
    }

    private void Update()
    {
        ClampCollectables();

        if (Input.GetKeyDown(KeyCode.B))
        {
            FileManager.DeleteAll(filePath);
        }
    }

    private void UpdateUI()
    {
        actualCollectableText.text = actualCollectable.ToString();
        
        Debug.Log(collectableTaken);
    }

    private void ClampCollectables()
    {
        if (actualCollectable >= totalCollectable)
        {
            actualCollectable = totalCollectable;
        }
    }
    
    public void AddCollectable()
    {
        actualCollectable += 1;
        collectableTaken += 1;
        
        SaveJsonData(this);
        UpdateUI();
    }
    
    
    #region SaveFunc

    private static string filePath = "SaveData.dat";
    private static void SaveJsonData(CollectableManager collectableManager)
    {
        SaveData saveData = new SaveData();
        collectableManager.PopulateSaveData(saveData);

        if (FileManager.WriteToFile(filePath, saveData.ToJson()))
        {
            Debug.Log("Save Succesful");
        }
    }
    
    public void PopulateSaveData(SaveData saveData)
    {
        saveData.collectables = collectableTaken;
    }

    private static void LoadJsonData(CollectableManager collectableManager)
    {
        if (FileManager.LoadFromFile(filePath, out var json))
        {
            SaveData saveData = new SaveData();
            saveData.LoadFromJson(json);
            
            collectableManager.LoadFromSaveData(saveData);
            Debug.Log("load succes");
        }
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        collectableTaken = saveData.collectables;

    }

    #endregion

}
