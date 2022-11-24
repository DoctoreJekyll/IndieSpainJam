using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int collectables;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string aJson)
    {
        JsonUtility.FromJsonOverwrite(aJson, this);
    }
}