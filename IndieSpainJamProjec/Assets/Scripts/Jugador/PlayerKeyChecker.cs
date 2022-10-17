using UnityEngine;

//Clase que se encarga de verificar si el jugador ha obtenido la llave del nivel
public class PlayerKeyChecker : MonoBehaviour
{
    [Header("[Values]")]
    public bool hasKey;


    public bool Check_PlayerHasKey()
    {
        return hasKey;
    }

}
