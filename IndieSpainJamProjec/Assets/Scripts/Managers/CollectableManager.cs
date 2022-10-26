using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    [SerializeField] private TMP_Text totalCollectableText;
    [SerializeField] private TMP_Text actualCollectableText;

    [SerializeField] private float totalCollectable;
    [SerializeField] private float actualCollectable;


    private void Start()
    {
        CollectableObject[] collects = FindObjectsOfType<CollectableObject>();
        totalCollectable = collects.Length;
    }

    private void Update()
    {
        SetTxt();
        ClampCollectables();
    }

    private void SetTxt()
    {
        totalCollectableText.text = totalCollectable.ToString();
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
    }
}
