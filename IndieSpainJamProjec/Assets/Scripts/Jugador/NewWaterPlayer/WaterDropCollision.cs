using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropCollision : MonoBehaviour
{
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            other.GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
        }
    }
}
