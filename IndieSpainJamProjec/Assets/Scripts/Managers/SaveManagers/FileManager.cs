using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{
    private static string namePath;

    private static string GetPath(string fileName)
    {
        namePath = Path.Combine(Application.persistentDataPath, fileName);

        return namePath;
    }
    
    public static bool WriteToFile(string fileName, string fileContent)
    {
        try
        {
            File.WriteAllText(GetPath(fileName), fileContent);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {GetPath(fileName)} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string fileName, out string result)
    {
        if (File.Exists(GetPath(fileName)))
        {
            try
            {
                result = File.ReadAllText(GetPath(fileName));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {GetPath(fileName)} with exception {e}");
                result = "";
                return false;
            }
        }
        else
        {
            Debug.LogError("Error al cargar");
            result = "";
            return false;
        }

    }

    public static void DeleteAll(string path)
    {
        if (File.Exists(GetPath(path)))
        {
            File.Delete(GetPath(path));
        }
    }
    
}
