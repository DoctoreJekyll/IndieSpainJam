using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveManager
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
    
    public static void SaveData()
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        string dataPath = GetDataPath();
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        
        GameData gameData = new GameData();
        
        formatter.Serialize(fileStream,gameData);
        fileStream.Close();
    }

    public static GameData LoadData()
    {
        string dataPath = GetDataPath();

        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            GameData gameData = (GameData)formatter.Deserialize(fileStream);
            fileStream.Close();
            return gameData;
        }
        else
        {
            Debug.LogError("No hay archivo guardado");
            return null;
        }
    }

    public static void DeleteAll()//Borrar todo
    {
        string dataPath = GetDataPath();

        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }
        
    }
        
}