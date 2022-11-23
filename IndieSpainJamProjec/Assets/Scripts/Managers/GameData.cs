using UnityEngine;

[System.Serializable]
public class GameData
{

    public static GameData _instance = new GameData();
    
    public int totalCollectables;//Para los coleccionables
    public float[] position = new float[3];//Para la posicion en caso de mapas grandes
    public bool hasPlayed;//Si ha jugado aparece boton continuar
    public int levelIndexToLoad;

    public GameData()
    {
        totalCollectables = 0;
        
        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
        
        hasPlayed = false;
        levelIndexToLoad = 0;
    }

}
