using System;
using UnityEngine;

public class SetFaceDirection : MonoBehaviour
{

    private GameObject targetToFace;
    
    private void OnEnable()
    {
        targetToFace = GameObject.FindWithTag("Lakitu");

        transform.localScale = targetToFace.transform.localScale;
    }
}
