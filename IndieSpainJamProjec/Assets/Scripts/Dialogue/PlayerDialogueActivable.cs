using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class PlayerDialogueActivable : MonoBehaviour
{
    public LocalizedString localString;

    [SerializeField] private GameObject chatPlayerBubble;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ChatBubble chatBubble = chatPlayerBubble.GetComponent<ChatBubble>();
            chatBubble.localString = localString;
            chatPlayerBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chatPlayerBubble.SetActive(false);
        }
    }
}
