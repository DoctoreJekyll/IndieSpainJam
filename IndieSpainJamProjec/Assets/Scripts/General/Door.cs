using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public SpriteRenderer topDoor;
    public SpriteRenderer mediumDoor, bottomDoor;
    public Sprite topOpenDoor, mediumOpenDoor, bottomOpenDoor;
    public Collider2D doorCollider;
    public GameObject backgroundDoor;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            OpenDoor();
    }

    public void OpenDoor()
    {
        doorCollider.enabled = false;
        backgroundDoor.SetActive(true);

        topDoor.sprite = topOpenDoor;
        mediumDoor.sprite = mediumOpenDoor;
        bottomDoor.sprite = bottomOpenDoor;
    }

}
