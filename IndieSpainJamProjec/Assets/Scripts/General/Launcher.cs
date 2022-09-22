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

    [SerializeField] private GameObject projectileArrow;
    [SerializeField] private LauncherDirection _launcherDirection = LauncherDirection.LEFT;

    private void LaunchProjectile()
    {
        GameObject arrow = Instantiate(projectileArrow, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = Vector2.right;
        
    }


}
