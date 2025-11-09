using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableObject : MonoBehaviour, ISaveable
{
    [SerializeField] private string id = System.Guid.NewGuid().ToString();
    protected SaveData data; 

    //Le asignamos una id
    public virtual string GetUniqueID() => id;

    //Guardamos los datos
    public abstract object CaptureData();

    //Cargamos los datos que previamente hemos guardado
    public abstract void RestoreData(object obj);

    public void SetId(string id)
    {
        this.id = id;
    }

    public string GetId()
    {
        return id;
    }
}
