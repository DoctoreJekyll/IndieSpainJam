using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Clase que se encarga de gestionar la cantidad de materia que tiene el jugador
public class Player_MatterManager : MonoBehaviour
{
    [Header("[References]")]
    public UI_MatterCanvas matterCanvas;

    [Header("[Configuration]")]
    public float initialMatterAmmount;
    public float obtainMatterRatio;
    public float consumeMatterRatio;
    public KeyCode matterKey;

    [Header("[Values]")]
    public float currentMatterAmmount;
    private bool canConsume;


    //Obtenemos lo componentes necesarios y establecemos una cantidad de materia inicial en el jugador
    private void Start()
    {
        matterCanvas = GameObject.FindGameObjectWithTag("Matter Canvas").GetComponent<UI_MatterCanvas>();
        
        currentMatterAmmount = initialMatterAmmount;
        matterCanvas.Refresh_MatterCanvas(currentMatterAmmount);
    }
    
    
    //TODO buscar alternativa para el new input system
    private void Update()
    {
        if (canConsume && currentMatterAmmount > 0)
            ConsumeMatter();

        //(DEBUG)
        if (Input.GetKey(KeyCode.M) && currentMatterAmmount < 100)
            ObtainMatter(obtainMatterRatio);
    }

    public void UseMateriaAction(InputAction.CallbackContext context)
    {
        if (context.performed && currentMatterAmmount > 0)
        {
            canConsume = true;
        }
        else if (context.canceled)
        {
            canConsume = false;
        }
    }

    //Obtiene materia
    public void ObtainMatter(float matterAmmount)
    {
        currentMatterAmmount += obtainMatterRatio * Time.deltaTime;

        if (currentMatterAmmount > 100)
            currentMatterAmmount = 100;

        matterCanvas.Refresh_MatterCanvas(currentMatterAmmount);
        Modify_PlayerSize();
    }


    //Consume materia y ejecuta su función dependiendo del estado actual del jugador
    private void ConsumeMatter()
    {
        //(TODO) Comprobamos el estado de la materia en el que nos encontramos en este momento y
        //ejecutamos la función correspondiente.

        currentMatterAmmount -= consumeMatterRatio * Time.deltaTime;

        if (currentMatterAmmount < 0)
            currentMatterAmmount = 0;

        matterCanvas.Refresh_MatterCanvas(currentMatterAmmount);
        Modify_PlayerSize();
    }


    //Modifica el tamaño del jugador dependiendo de la cantidad de materia que tenga
    private void Modify_PlayerSize()
    {
        //(TODO) Modificar el tamaño del jugador teniendo en cuenta la cantidad de materia (tamaño mínimo 50%)
    }
}
