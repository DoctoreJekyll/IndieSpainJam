using UnityEngine;
using Cinemachine;

public class CinemachineNoise : MonoBehaviour
{

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float shakeTimer;

    public static CinemachineNoise instance;

    private void Awake()
    {

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CreateSingleton();
    }
    
    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Update()
    {

        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin channelPerlin =
                    _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                channelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
        
    }

}
