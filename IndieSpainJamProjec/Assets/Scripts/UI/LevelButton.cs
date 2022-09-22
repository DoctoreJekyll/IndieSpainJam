using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Header("[References]")]
    public string levelName;


    public void OnClick_Level()
    {
        SceneManager.LoadScene(levelName);
    }
}
