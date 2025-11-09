using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Creamos una interface para objetos que se pueden guardar
public interface ISaveable
{
    string GetUniqueID();
    object CaptureData();
    void RestoreData(object data);
}
