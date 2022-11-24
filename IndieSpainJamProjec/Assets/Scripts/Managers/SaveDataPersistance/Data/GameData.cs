using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Clase que se encarga de recoger todos los datos que vamos a necesitar guardar o cargar
[System.Serializable]
public class GameData
{
    public int totalCollectablesTakenByPlayer;

    
    //Inicialmente usaremos este constructor para generar valores default para el new game
    public GameData()
    {
        totalCollectablesTakenByPlayer = 0;
    }

}
