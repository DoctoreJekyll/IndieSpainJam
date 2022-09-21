using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de que el jugador pueda obtener la llave y esta le siga
public class LevelKey : MonoBehaviour
{
    [Header("[References]")]
    public GameObject player;

    [Header("[Configuration]")]
    public float followSpeed;
    public float followMinDistance;

    [Header("[Values]")]
    public bool keyObtained;
    public bool followingPlayer;
    private Transform lastPosition;
    public List<Transform> playerPositionList;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPositionList = new List<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keyObtained == false)
        {
            keyObtained = true;
            followingPlayer = true;
            player.GetComponent<PlayerKeyChecker>().hasKey = true;
            TimeImpact.instance.ImpactTime(TimeImpact.SlowMagnitude.MEDIUM);
        }
    }

    private void Update()
    {
        if (followingPlayer && player != null)
        {
            float playerDistance = Vector2.Distance(transform.position, player.transform.position);

            //Si el jugador se aleja de la llave, guardamos el camino que est� siguiendo
            if(playerDistance > followMinDistance)
            {
                playerPositionList.Add(player.transform);
                transform.position = Vector3.Lerp(transform.position, playerPositionList[0].transform.position, followSpeed * Time.deltaTime);
                lastPosition = playerPositionList[0];
                playerPositionList.RemoveAt(0);
            }

            //Si hay al menos una posici�n en al lista el jugador se ha movido, movemos la llave hasta la siguiente posici�n
            if (playerPositionList.Count > 0)
                transform.position = Vector3.Lerp(transform.position, playerPositionList[0].transform.position, followSpeed * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, lastPosition.position, followSpeed * Time.deltaTime);
        }
    }
}
