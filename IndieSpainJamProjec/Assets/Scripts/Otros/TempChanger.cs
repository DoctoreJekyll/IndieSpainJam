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
    private PlayerStatesManager _playerStatesManager;
    


    // Start is called before the first frame update
    void Start()
    {
        tempManager = GameObject.FindGameObjectWithTag("Temp Manager").GetComponent<TempManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
            tempManager.ModifyTemperature(temperatura, (intensidad + (potencia * 0.5f)));

        }
    }

    public bool isPlayerOn;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            _audioSource.volume = 0.5f;
            isPlayerOn = true;
            if (temperatura > 0)
            {
                _audioSource.PlayOneShot(_audioClipHot);
            }
            else if (temperatura < 0)
            {
                _audioSource.PlayOneShot(_audioClipFrost);  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
            StartCoroutine(TestCoroutine());
        }
    }
    
    private IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (!isPlayerOn)
        {
            float startVolume = _audioSource.volume;
            while (_audioSource.volume > 0)
            {
                _audioSource.volume -= startVolume * Time.deltaTime / 0.5f;
                yield return new WaitForFixedUpdate();

            }
            //_audioSource.Stop();
        }
        

    }
    
    
}
