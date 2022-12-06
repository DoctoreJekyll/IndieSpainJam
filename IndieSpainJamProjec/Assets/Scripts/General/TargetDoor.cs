using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que se encarga de comprobar si el jugador ha conseguido la llave para completar el nivel
public class TargetDoor : MonoBehaviour
{
    [Header("[References]")]
    public PlayerKeyChecker keyChecker;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        keyChecker = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerKeyChecker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckPlayerKey(collision);
        }
            
    }

    private void CheckPlayerKey(Collider2D col)
    {
        if (keyChecker.Check_PlayerHasKey() == true)
        {
            LevelManager.instance.LevelCompleted();
            _audioSource.PlayOneShot(_audioSource.clip);
            Rigidbody2D rb2d = col.gameObject.GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.zero;
        }
            
    }


}
