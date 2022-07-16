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

    public List<DieStats> clearDice(){
        List<DieStats> result = new List<DieStats>();
        int[] outcome = new int[(int)EffectType.NUM_EFFECTS];
        foreach (GameObject go in GOs)
        {
            DieScript dscript = go.GetComponent(typeof(DieScript)) as DieScript;
            result.Add(dscript.stats);
            Destroy(go);
        }
        GOs.Clear();
        return result;
    }

    public int[] readDice(){
        int[] outcome = new int[(int)EffectType.NUM_EFFECTS];
        foreach (GameObject go in GOs)
        {
            DieScript dscript = go.GetComponent(typeof(DieScript)) as DieScript;
            int whatEffect = (int)(dscript.curEffect().effectType);
            outcome[whatEffect] += dscript.curEffect().value;
        }
        return outcome;
    }

    public GameObject unpackDie(DieStats packed, Vector3 pos)
    {
        GameObject go = Instantiate(diePrefabs[packed._numSides - 3], pos, Quaternion.identity);
        GOs.Add(go);
        //Debug.Log("GOs.Count = " + GOs.Count);
        DieScript ds = go.GetComponent(typeof(DieScript)) as DieScript;
        ds.init(packed);
        return go;
    }
}

