using System;
using System.Collections;
using UnityEngine;

//Clase que se encarga de modificar la temperatura del jugador al entrar en contacto
public class TempChanger : MonoBehaviour
{
    TempManager tempManager;
    
    private enum ChangerType
    {
        NORMAL,
        WEAK,
    }

    private enum TempType
    {
        ICE,
        HOT
    }

    [SerializeField] private ChangerType type;
    [SerializeField] private TempType tempType;

    [Header("[Configuration]")]
    public int temperatura;
    public int intensidad;
    public bool distanceModifier;

    [Header("[Sounds]")] 
    [SerializeField] private AudioClip _audioClipHot;
    [SerializeField] private AudioClip _audioClipFrost;
    private AudioSource _audioSource;
    private PlayerStatesManager _playerStatesManager;
    
    [Header("[Values]")]
    public bool isPlayerOn;


    //Obtenemos los componentes que necesitamos
    void Start()
    {
        tempManager = GameObject.FindObjectOfType<TempManager>();
        _audioSource = GetComponent<AudioSource>();
    }


    //Si el jugador entra en contacto reproducimos sonidos de cambio de temperatura y efectos de Postprocessing
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
            _audioSource.volume = 0.5f;

            if (temperatura > 0)
            {
                _audioSource.PlayOneShot(_audioClipHot);
                PostProcessController.instance.VignetterCoroutine(Color.red);
                
            }
            else if (temperatura < 0)
            {
                _audioSource.PlayOneShot(_audioClipFrost);  
                PostProcessController.instance.VignetterCoroutine(Color.blue);
            }
        }
    }


    //Si el jugador se queda dentro, modificamos su temperatura
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case ChangerType.NORMAL:
                    if (distanceModifier) //Intensificamos el cambio de temperatura teniendo en cuenta la distancia del jugador hacia este GameObject
                    {
                        float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
                        tempManager.ModifyTemperature(temperatura, (intensidad + (potencia * 0.5f)));
                    }
                    else
                    {
                        tempManager.ModifyTemperature(temperatura, intensidad);
                    }
                    break;
            
                case ChangerType.WEAK://Si es d??bil se llama a otro m??todo que clampea las temperaturas
                    switch (tempType)
                    {
                        case TempType.ICE:
                            if (distanceModifier)
                            {
                                float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
                                tempManager.ModifyTemperatureWithClampIce(temperatura, (intensidad + (potencia * 0.5f)));
                            }
                            else
                            {
                                tempManager.ModifyTemperatureWithClampIce(temperatura, intensidad);
                            }
                            break;
                        
                        case TempType.HOT:
                            if (distanceModifier)
                            {
                                float potencia = 1 / Vector3.Distance(this.transform.position, collision.transform.position);
                                tempManager.ModifyTemperatureWithClampHot(temperatura, (intensidad + (potencia * 0.5f)));
                            }
                            else
                            {
                                tempManager.ModifyTemperatureWithClampHot(temperatura, intensidad);
                            }
                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;

                default:
                    break;
            }
        }
    }


    //Si el jugador pierde el contacto dejamos de reproducir sonidos y efectos de Postprocessing
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOn = false;
            StartCoroutine(FadeOutVolumeCoroutine());
            PostProcessController.instance.NoVignetteCoroutine();
        }
    }
    
    //Baja el volumen del audiosource poco a poco
    private IEnumerator FadeOutVolumeCoroutine()
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
        }
    }
    
    
}
