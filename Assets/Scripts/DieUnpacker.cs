using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUnpacker : MonoBehaviour
{
    public GameObject diePrefab;
    
    public GameObject unpackDie(DieStats2 packed, Vector3 pos)
    {
        // add packed effects to the die here
        GameObject go = Instantiate(diePrefab, pos, Quaternion.identity);
        DieScript ds = go.GetComponent(typeof(DieScript)) as DieScript;
        ds.stats = packed;
        return go;
    }
}

