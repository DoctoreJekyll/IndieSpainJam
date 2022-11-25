using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler : MonoBehaviour
{

    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "Capibara";

    //Constrcr para crear namedatas
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
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
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))//Buscamos la ruta del archivo y lo abrimos
                {
                    using (StreamReader reader = new StreamReader(stream))//Leemos datos
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptionDecryption(dataToLoad);
                }
                
                //Deserializar el JSON
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when try to load datas: " + fullPath + "\n" + e);
            }
        }

        return loadedData;//devolemos los datos
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

            //encriptamos datos si queremos
            if (useEncryption)
            {
                dataToStore = EncryptionDecryption(dataToStore);
            }

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

    //Elevamos cada valor de los datos al valor de la palabra que pasamos por código. EJ: el valor del valor de los collectables por "Capibara".
    private string EncryptionDecryption(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }

    //Borra TODOS los datos, cuidado.
    public void Delete(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        
        if (File.Exists(fullPath))
        {
            Debug.Log("Delete" + fullPath);
            File.Delete(fullPath);
        }
    }
}
