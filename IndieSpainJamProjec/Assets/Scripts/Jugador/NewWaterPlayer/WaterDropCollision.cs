using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterDropCollision : MonoBehaviour
{

    private ParticleSystem waterParticleSystem;

    private void OnEnable()
    {
        waterParticleSystem = GetComponent<ParticleSystem>();
        waterParticleSystem.Stop();
    }

    public void LaunchWater(InputAction.CallbackContext context)//Llamamos a este metodo dentro del componente input action del playermanager 
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (context.performed)
            {
                waterParticleSystem.Play();
                Debug.Log("PlayParticle");
            }
            else if (context.canceled)
            {
                waterParticleSystem.Stop();
                Debug.Log("stopParticle");
            }
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Activable"))
        {
            if (other.GetComponent<IAffectedByWaterDrop>() != null)
            {
                IAffectedByWaterDrop iAffectedByWaterDrop = other.GetComponent<IAffectedByWaterDrop>();
                iAffectedByWaterDrop.DoStuffsWhenWaterTouch();
            }

        }
    }
}
