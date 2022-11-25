using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistance> dataPersistancesObjs;
    private FileDataHandler dataHandler;
    public static DataPersistanceManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one datapersistance on scene");
        }

        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        //Cargamos siempre al iniciar, si no existen valores, carga default, si existen, carga el normal.
        this.dataPersistancesObjs = FindAllDataPersistanceObjs();
        LoadGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            dataHandler.Delete(this.gameData);
        }
    }

    private void NewGame()
    {
        //Instanciamos un constructor con valores default
        //Tambien podemos usar esto para borrar todos los datos con el .Delete
        this.gameData = new GameData();
    }

    private void LoadGame()
    {
        //Load save data
        this.gameData = dataHandler.Load();
        
        if (this.gameData == null)
        {
            Debug.Log("No data found. Iniciando data default");
            NewGame();
        }

        //Buscamos todos los datos cargados y cargamos
        foreach (IDataPersistance dataPersistance in dataPersistancesObjs)
        {
            dataPersistance.LoadData(gameData);
        }
        
        Debug.Log("Load collectables" + gameData.totalCollectablesTakenByPlayer);
    }

    private void SaveGame()
    {
        foreach (IDataPersistance dataPersistance in dataPersistancesObjs)
        {
            dataPersistance.SaveData(ref gameData);
        }
        
        dataHandler.Save(gameData);
        
        Debug.Log("Save collectables" + gameData.totalCollectablesTakenByPlayer);
    }

    //De base guardamos cuando se cierre la aplicación TODO- Ver si mantenemos esto o no.
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjs()
    {
        //Buscamos todos los objetos que usen la interface
        IEnumerable<IDataPersistance> persistancesObjs =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(persistancesObjs);
    }
}
