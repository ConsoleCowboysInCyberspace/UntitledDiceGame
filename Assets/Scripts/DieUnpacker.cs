using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUnpacker : MonoBehaviour
{
    public List<GameObject> diePrefabs;
    public List<GameObject> GOs;

    void Start() {
        GOs = new List<GameObject>();
    }

    public List<DieStats> ClearDice(){
        List<DieStats> result = new List<DieStats>();
        foreach (GameObject go in GOs)
        {
            DieScript dscript = go.GetComponent(typeof(DieScript)) as DieScript;
            result.Add(dscript.stats);
            Destroy(go);
        }
        GOs.Clear();
        return result;
    }

    public GameObject unpackDie(DieStats packed, Vector3 pos)
    {
        GameObject go = Instantiate(diePrefabs[packed._numSides - 3], pos, Quaternion.identity);
        GOs.Add(go);
        DieScript ds = go.GetComponent(typeof(DieScript)) as DieScript;
        ds.init(packed);
        return go;
    }
}

