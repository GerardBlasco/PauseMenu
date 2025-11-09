using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystemController : MonoBehaviour
{
    private bool hasLoaded = false;

    public static SaveSystemController instance;
    private static string saveFolder => Application.persistentDataPath + "/Saves";
    private static string saveFile => saveFolder + "/SaveData.dat";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!hasLoaded)
        {
            hasLoaded = true;
            LoadGame();
        }
    }

    // Metdo de guardado
    public static void SaveGame()
    {
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }

        //Guardamos los datos que hay en la clase SaveData
        TempData tempData = new TempData();

        //Hacemos una lista de los ISaveable que tenemos 
        ISaveable[] saveables = FindObjectsOfType<MonoBehaviour>(true) as ISaveable[];

        //Verificamos si los MonoBehaviours son isaveables y en ese caso guardamos la informacion
        foreach (MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>(true))
        {
            if (mono is ISaveable saveable)
            {
                string id = saveable.GetUniqueID();
                object data = saveable.CaptureData();
                tempData.allData[id] = data;
            }
        }

        //Lo pasamos a binario
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(saveFile, FileMode.Create))
        {
            formatter.Serialize(stream, tempData);
        }

        Debug.Log($"✅ Juego guardado en: {saveFile}");
    }

    // === CARGAR TODO ===
    public static void LoadGame()
    {
        if (!File.Exists(saveFile))
        {
            Debug.LogWarning("❌ No se encontró archivo de guardado.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        TempData tempData;

        using (FileStream stream = new FileStream(saveFile, FileMode.Open))
        {
            tempData = formatter.Deserialize(stream) as TempData;
        }

        //Verificamos si los MonoBehaviours son isaveables y en ese caso cargamos la informacion
        foreach (MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>(true))
        {
            if (mono is ISaveable saveable)
            {
                string id = saveable.GetUniqueID();
                if (tempData.allData.ContainsKey(id))
                {
                    saveable.RestoreData(tempData.allData[id]);
                }
            }
        }

        Debug.Log("✅ Juego cargado correctamente");
    }
}
