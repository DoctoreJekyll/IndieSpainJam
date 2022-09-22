using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    enum LauncherDirection
    {
        TOP,
        BOT,
        LEFT,
        RIGTH
    }

    [SerializeField] private float arrowSpeed;
    [SerializeField] private GameObject projectileArrow;
    [SerializeField] private LauncherDirection _launcherDirection = LauncherDirection.LEFT;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile(Vector2.up, Vector3.zero);
        }
    }

    private void LaunchProjectile(Vector2 direction, Vector3 rotationValues)
    {
        GameObject arrow = Instantiate(projectileArrow, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = direction * (arrowSpeed * Time.deltaTime);
        arrow.GetComponent<Transform>().eulerAngles = rotationValues;
    }


}
