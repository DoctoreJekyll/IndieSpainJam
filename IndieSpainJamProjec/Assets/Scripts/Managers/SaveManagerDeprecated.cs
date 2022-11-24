using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveManagerDeprecated
{
    private static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }

    private static string GetDataPath()
    {
        string dataPath = Application.persistentDataPath + "/game.save";

        return dataPath;
    }
    
    public static void BinarySaveData()
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        string dataPath = GetDataPath();
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        
        GameDataDeprecated gameDataDeprecated = new GameDataDeprecated();
        
        formatter.Serialize(fileStream,gameDataDeprecated);
        fileStream.Close();
    }

    public static GameDataDeprecated BinaryLoadData()
    {
        string dataPath = GetDataPath();

        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            GameDataDeprecated gameDataDeprecated = (GameDataDeprecated)formatter.Deserialize(fileStream);
            fileStream.Close();
            return gameDataDeprecated;
        }
        else
        {
            Debug.LogError("No hay archivo guardado");
            return null;
        }
    }

    public static void BinaryDeleteAll()//Borrar todoFi
    {
        string dataPath = GetDataPath();

        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }
        
    }
        
}