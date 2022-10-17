using System.Collections;
using UnityEngine;

//Clase que se encarga de producir un shake a la cámara
public class CameraShake : MonoBehaviour
{
    public enum ShakeMagnitude { NULL, SMALL, MEDIUM, BIG, HUGE }
    public static CameraShake instance;

    [Header("[References]")]
    [SerializeField] private Vector3 originalPos;

    [Header("[Configuration]")]
    [SerializeField] private float shakeDuration;
    [SerializeField] private float smallMagnitude;
    [SerializeField] private float normalMagnitude, bigMagnitude, hugeMagnitude;

    private void Awake()
    {
        CreateSingleton();
    }
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }


    private void Start()
    {
        originalPos = gameObject.transform.localPosition;
    }

    public void ShakeCamera(ShakeMagnitude newShakeMagnitude)
    {
        float magnitude = 0;

        switch (newShakeMagnitude)
        {
            case ShakeMagnitude.NULL:
                magnitude = 0;
                break;
            case ShakeMagnitude.SMALL:
                magnitude = smallMagnitude;
                break;
            case ShakeMagnitude.MEDIUM:
                magnitude = normalMagnitude;
                break;
            case ShakeMagnitude.BIG:
                magnitude = bigMagnitude;
                break;
            case ShakeMagnitude.HUGE:
                magnitude = hugeMagnitude;
                break;
        }

        StartCoroutine(Coroutine_ShakeCamera(shakeDuration, magnitude));
    }

    IEnumerator Coroutine_ShakeCamera(float duration, float magnitude)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + xOffset, gameObject.transform.localPosition.y + yOffset, originalPos.z);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        
    }
}
