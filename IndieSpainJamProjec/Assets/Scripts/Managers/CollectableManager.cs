using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private CollectableObject[] collects;

    [SerializeField] private TMP_Text totalCollectableText;
    [SerializeField] private TMP_Text actualCollectableText;

    [SerializeField] private float totalCollectable;
    [SerializeField] private float actualCollectable;
    

    private void Awake()
    {
        collects = FindObjectsOfType<CollectableObject>();
    }

    private void Start()
    {
        totalCollectable = collects.Length;
        
        totalCollectableText.text = totalCollectable.ToString();
    }

    private void Update()
    {
        ClampCollectables();
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

        UpdateUI();
    }
    

}
