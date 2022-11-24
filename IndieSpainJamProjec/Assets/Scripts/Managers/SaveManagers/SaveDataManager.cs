using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    
    //TODO GENERAR UNA CLASE GENERICA PARA PODER USAR ESTOS METODOS EN CADA OBJETO SIN REPETIR CODIGO
    //TODO INVESTIGAR CLASE "XOR" PARA EL ENCRIPTADO SI FUESE NECESARIO
    
    public static void SaveJsonData<T>(IEnumerable<ISaveable> aSaveables)
    {
        SaveData sd = new SaveData();
        foreach (var saveable in aSaveables)
        {
            saveable.PopulateSaveData(sd);
        }

        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save successful");
        }
    }
    
    public static void LoadJsonData(IEnumerable<ISaveable> aSaveables)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            foreach (var saveable in aSaveables)
            {
                saveable.LoadFromSaveData(sd);
            }
            
            Debug.Log("Load complete");
        }
    }
}
