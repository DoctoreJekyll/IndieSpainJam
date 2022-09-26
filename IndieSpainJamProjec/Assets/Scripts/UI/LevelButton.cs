using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button levelButton;
    public GameObject locker, textoNivel;
    public string levelName;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("Level 1", 1);

        if (PlayerPrefs.GetInt(levelName, 0) == 1) //El nivel es accesible
        {
            levelButton.interactable = true;
            locker.SetActive(false);
            textoNivel.SetActive(true);
        }
        else
        {
            levelButton.interactable = false;
            locker.SetActive(true);
            textoNivel.SetActive(false);
        }
    }

    public void OnClick_Level()
    {
        SceneManager.LoadScene(levelName);
    }
}
