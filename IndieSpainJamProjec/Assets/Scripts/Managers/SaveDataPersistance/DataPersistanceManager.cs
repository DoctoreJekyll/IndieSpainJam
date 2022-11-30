using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Debugging")] 
    [SerializeField] private bool initializeData = false;
    
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryp;

    [Header("Auto Save Config")] 
    [SerializeField] private float autoSaveTimeSeconds = 120f;
    private Coroutine autoSaveCoroutine;

    public GameData gameData;

    private List<IDataPersistance> dataPersistancesObjs;
    private FileDataHandler dataHandler;
    public static DataPersistanceManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogAssertion("More than one datapersistance on scene");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
        
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryp);
        //this.dataHandler = gameObject.AddComponent<FileDataHandler>();
        //this.dataHandler.ConstructorFileData(Application.persistentDataPath, fileName, useEncryp);
        //TODO Ver si esto da algun problema
        this.dataPersistancesObjs = FindAllDataPersistanceObjs();
    }

    public void NewGame()
    {
        //Instanciamos un constructor con valores default
        //Tambien podemos usar esto para borrar todos los datos con el .Delete
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load save data
        this.gameData = dataHandler.Load();

        //Solo debug, si no hay datos pero queremos probar escenas, crea un nuevo archivo.
        if (this.gameData == null && initializeData)
        {
            NewGame();
        }
        
        if (this.gameData == null)
        {
            Debug.Log("No data found. Iniciando data default");
            //NewGame();//Si no tenemos un archivo de guardado no hacemos nada.
            return;
        }

        //Buscamos todos los datos cargados y cargamos
        foreach (IDataPersistance dataPersistance in dataPersistancesObjs)
        {
            dataPersistance.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //Si no tenemos savedata, error
        if (this.gameData == null)
        {
            //NewGame();Si no tenemos datos que guardar. Error, esto puede cambiar en base a generar seguridad para la version final
            Debug.LogWarning("No data to save");
            return;
        }
        
        foreach (IDataPersistance dataPersistance in dataPersistancesObjs)
        {
            dataPersistance.SaveData(gameData);
        }
        
        dataHandler.Save(gameData);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    //TODO- Tener en cuenta que ahora mismo cargamos datos cuando una escena es cargada y guardamos al descargar una escena.
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Cargamos siempre al iniciar, si no existen valores, carga default, si existen, carga el normal.
        this.dataPersistancesObjs = FindAllDataPersistanceObjs();
        LoadGame();

        if (autoSaveCoroutine != null)
        {
            StopCoroutine(AutoSave());
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    //TODO- Ver donde generamos el guardar partida.
    //Creo que esto no funciona bien, hay que ver donde generamos el guardar.
    public void OnSceneUnLoaded(Scene scene)
    {
        SaveGame();
    }

    //De base guardamos cuando se cierre la aplicación TODO- Ver si mantenemos esto o no.(Probablemente si)
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjs()
    {
        //Buscamos todos los objetos que usen la interface
        IEnumerable<IDataPersistance> persistancesObjs =
            FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistance>();

        return new List<IDataPersistance>(persistancesObjs);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    //TODO- Ver si usamos o no autosave
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
        }
    }
    
}
