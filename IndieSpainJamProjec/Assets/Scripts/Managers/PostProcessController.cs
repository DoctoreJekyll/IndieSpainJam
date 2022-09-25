using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{

    public static PostProcessController instance;
    
    [SerializeField] private Volume _volume;
    [SerializeField] private Vignette _vignette;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        _volume.profile.TryGet(out _vignette);
    }

    private void CreateSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    
    public void VignetterCoroutine(Color color)
    {
        StartCoroutine(ScaleVignette(color));
    }

    public void NoVignetteCoroutine()
    {
        StartCoroutine(NoScaleVignette());
    }


    private IEnumerator ScaleVignette(Color color)
    {
        _vignette.color.overrideState = true;
        _vignette.color.Override(color);
        float maxValue = 0.25f;
        while (_vignette.intensity.value < maxValue)
        {
            _vignette.intensity.value += Time.unscaledDeltaTime * 0.5f;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator NoScaleVignette()
    {
        
        while (_vignette.intensity.value > 0)
        {
            _vignette.intensity.value -= Time.unscaledDeltaTime * 0.5f;
            yield return new WaitForEndOfFrame();
        }
        
        
    }
    
}
