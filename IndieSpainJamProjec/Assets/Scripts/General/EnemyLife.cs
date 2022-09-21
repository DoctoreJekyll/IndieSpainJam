using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IActivable
{

    [SerializeField] private int life;

    private void Start()
    {
        life = 1;
    }

    private void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void LoseLife(int amount)
    {
        life -= amount;
    }

    public void DoActivate()
    {
        Debug.Log("perder vida noeke");
        LoseLife(1);
    }
}
