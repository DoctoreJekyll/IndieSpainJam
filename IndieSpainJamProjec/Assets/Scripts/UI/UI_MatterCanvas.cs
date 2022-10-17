using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Clase que se encarga de actualizar la UI con la cantidad de materia que tiene el jugador
public class UI_MatterCanvas : MonoBehaviour
{
    [Header("[References]")]
    public TextMeshProUGUI matterAmmountText;

    [Header("[Values]")]
    [SerializeField] private float currentMatterAmmount;


    //Actualiza la UI con la cantidad de materia que tiene el jugador
    public void Refresh_MatterCanvas(float newMatterAmmount)
    {
        currentMatterAmmount = newMatterAmmount;
        matterAmmountText.text = "Matter: " + currentMatterAmmount.ToString("F0") + "%";
    }
}
