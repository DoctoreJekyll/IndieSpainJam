using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakituPatch : MonoBehaviour
{
    [SerializeField] private Transform lakituTransform;

    private void OnEnable()
    {
        transform.position = lakituTransform.position;
    }
}
