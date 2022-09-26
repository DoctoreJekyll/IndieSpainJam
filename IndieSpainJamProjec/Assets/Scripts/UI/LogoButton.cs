using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoButton : MonoBehaviour
{
    public string url;

    public void OnClick_LogoButton()
    {
        Application.OpenURL(url);
    }
}
