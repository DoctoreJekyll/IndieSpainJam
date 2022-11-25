using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler : MonoBehaviour
{

    private string dataDirPath = "";
    private string dataFileName = "";

    //Constrcr para crear namedatas
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                //Deserializar el JSON
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when try to load datas: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //Creamos el directorio
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);

            //Serializamos(pasar del archivo C# a JSON)
            string dataToStore = JsonUtility.ToJson(data, true);

            //Escribimos el serializado del contenido del JSON, la sintaxis es rara de cojones pero viene asi en la documentacion
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error ocurred when try to save datas: " + fullPath + "\n" + e);
        }
    }

    //Borra TODOS los datos, cuidado.
    public void Delete(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}
