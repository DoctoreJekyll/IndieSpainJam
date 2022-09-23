using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = System.Random;

public class ChatBubble : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private TMP_Text textMeshPro;

    [Header("Text")]
    [TextArea(3,6)]
    [SerializeField] private string textToSpeech;
    
    [Header("Padding Box Values")]
    [SerializeField] private float paddingX;
    [SerializeField] private float paddingY;

    [Header("Sounds")] 
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioClip dialogueOnClip;
    [SerializeField] private AudioClip dialogueOffClip;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
    }

    private void OnEnable()
    {
        _audioSource.PlayOneShot(dialogueOnClip);
        StartCoroutine(ShowText());
    }

    private void OnDisable()
    {
        _audioSource.PlayOneShot(dialogueOffClip);
    }

    private void Update()
    {
        SetupText();
    }

    private void SetupText()
    {
        //textToSpeech.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(paddingX, paddingY);
        backgroundSpriteRenderer.size = textSize + padding;

        //Vector3 offset = new Vector3(-3f, 0f);
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f);

    }
    
    private IEnumerator ShowText()
    {
        textMeshPro.text = "";
        
        foreach (char ch in textToSpeech)
        {
            textMeshPro.text += ch;
            _audioSource.PlayOneShot(_clip);
            yield return new WaitForSeconds(0.075f);
        }
        
    }
    
}
