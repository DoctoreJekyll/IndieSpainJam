using UnityEngine;
using Random = UnityEngine.Random;

public class IcePlayerSounds : MonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;
    
    [Header("Sounds")] 
    [SerializeField] private AudioClip iceAppearSong;
    [SerializeField] private AudioClip iceImpactClip;
    [SerializeField] private AudioClip breakGroundClip;
    [SerializeField] private AudioClip deadClip;
    [SerializeField] private AudioClip step;
    
    
    [Header("Effects")]
    [SerializeField] private GameObject particle;

    //Dead stuffs
    private bool checkDead;
    private PlayerDeath _playerDeath;
    
    private void OnEnable()
    {
        CameraShake.instance.ShakeCamera(CameraShake.ShakeMagnitude.MEDIUM);
        _audioSource.PlayOneShot(iceAppearSong);
        particle.SetActive(true);
        checkDead = false;
    }

    private void Awake()
    {
        _playerDeath = GetComponent<PlayerDeath>();
    }

    private void Update()
    {
        if (_playerDeath.dead == true && checkDead == false)
        {
            DeadClip();
            checkDead = true;
        }
    }

    public void Step()//Este metodo se llama en el animator para que reproduzca sus pasos
    {
        _audioSource.pitch = Random.Range(0.9f, 1f);
        _audioSource.PlayOneShot(step);
    }


    public void IceImpactClip()
    {
        _audioSource.pitch = Random.Range(0.9f, 1f);
        _audioSource.PlayOneShot(iceImpactClip);
    }

    public void BreakGroundClip()
    {
        _audioSource.PlayOneShot(breakGroundClip);
    }

    public void DeadClip()
    {
        _audioSource.PlayOneShot(deadClip);
    }

}
