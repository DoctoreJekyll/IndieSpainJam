using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterDropCollision : MonoBehaviour
{

    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    public void LaunchWater(InputAction.CallbackContext context)//Llamamos a este metodo dentro del componente input action del playermanager 
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (context.performed)
            {
                particleSystem.Play();
                Debug.Log("PlayParticle");
            }
            else if (context.canceled)
            {
                particleSystem.Stop();
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
