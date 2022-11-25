using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour, IDataPersistance
{
    private CollectableObject[] collects;
    public GameObject[] allCollectables;

    [SerializeField] private TMP_Text totalCollectableText;
    [SerializeField] private TMP_Text actualCollectableText;

    [SerializeField] private int totalCollectable;
    [SerializeField] private int actualCollectable;
    private int totalPlayerCollectables;
    

    private void Awake()
    {
        collects = FindObjectsOfType<CollectableObject>();
        allCollectables = GameObject.FindGameObjectsWithTag("Collectable");
    }

    private void Start()
    {
        totalCollectable = collects.Length;
        
        totalCollectableText.text = totalCollectable.ToString();
        
        Test();
    }

    private void Update()
    {
        ClampCollectables();
    }

    private void Test()
    {
        for (int i = 0; i < allCollectables.Length; i++)
        {
            if (!allCollectables[i].activeInHierarchy)
            {
                Debug.Log("suma");
                actualCollectable += 1;
                UpdateUI();
            }
        }
    }
    
    private void UpdateUI()
    {
        actualCollectableText.text = actualCollectable.ToString();
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
        totalPlayerCollectables += 1;

        UpdateUI();
    }


    public void LoadData(GameData data)
    {
        this.totalPlayerCollectables = data.totalCollectablesTakenByPlayer;
    }

    public void SaveData(ref GameData data)
    {
        data.totalCollectablesTakenByPlayer = this.totalPlayerCollectables;
    }
}
