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

    [SerializeField] private float fireRate;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private GameObject projectileArrow;
    [SerializeField] private LauncherDirection _launcherDirection = LauncherDirection.LEFT;
    
    private void Start()
    {
        InvokeRepeating(nameof(SwitchBetweenLauncherTypes), 1, fireRate);
    }

    private void SwitchBetweenLauncherTypes()
    {
        switch (_launcherDirection)
        {    
            case LauncherDirection.TOP:
                LaunchProjectile(Vector2.up, Vector3.zero);
                break;
            case LauncherDirection.BOT:
                LaunchProjectile(Vector2.down, new Vector3(0f,0f,-180f));
                break;
            case LauncherDirection.LEFT:
                LaunchProjectile(Vector2.left, new Vector3(0f,0f,90f));
                break;
            case LauncherDirection.RIGTH:
                LaunchProjectile(Vector2.right, new Vector3(0f,0f,-90f));
                break;
            
        }
    }

    private void LaunchProjectile(Vector2 direction, Vector3 rotationValues)
    {
        GameObject arrow = Instantiate(projectileArrow, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
        arrow.GetComponent<Transform>().eulerAngles = rotationValues;
    }


}
