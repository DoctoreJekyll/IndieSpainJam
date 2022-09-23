using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChanger : MonoBehaviour
{
    public int temperatura;
    public int intensidad;
    TempManager tempManager;

    [Header("Sounds")] 
    [SerializeField] private AudioClip _audioClipHot;
    [SerializeField] private AudioClip _audioClipFrost;
    private AudioSource _audioSource;
    


    // Start is called before the first frame update
    void Start()
    {

        TakeSoundAndGet();
        tempManager = GameObject.FindGameObjectWithTag("Temp Manager").GetComponent<TempManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
            tempManager.ModifyTemperature(temperatura, (intensidad + (potencia * 0.5f)));
            
        }
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         _audioSource.Play();
    //     }
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         _audioSource.Stop();
    //     }
    // }


    private void TakeSoundAndGet()
    {
        _audioSource = GetComponent<AudioSource>();
        if (this.gameObject.CompareTag("Hot"))
        {
            _audioSource.clip = _audioClipHot;
        }
        else if (this.gameObject.CompareTag("Frost"))
        {
            _audioSource.clip = _audioClipFrost;
        }
    }
    
}
