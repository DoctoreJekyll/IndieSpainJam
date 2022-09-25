using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTempModifier : MonoBehaviour
{
    public TempManager tempManager;
    public float modifier, instensity;



    private void Update()
    {
        tempManager.ModifyTemperature(modifier, instensity);
    }
}
