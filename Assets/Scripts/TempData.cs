using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//En este script guardaremos los datos del resto de datas para tenerlos en un solo archivo a la hora de pasarlo a binario,
//de esta manera podermos añadir todos los que queramos sin necesidad de estar añadiendolos a mano 
[System.Serializable]
public class TempData
{
    public Dictionary<string, object> allData = new Dictionary<string, object>();
}
