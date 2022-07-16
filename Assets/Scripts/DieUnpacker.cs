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
        int[] outcome = new int[(int)EffectType.NUM_EFFECTS];
        foreach (GameObject go in GOs)
        {
            DieScript dscript = go.GetComponent(typeof(DieScript)) as DieScript;
            int whatEffect = (int)(dscript.curEffect().effectType);
            outcome[whatEffect] += dscript.curEffect().value;
            result.Add(dscript.stats);
    Debug.Log("23 result.Count = " + result.Count);
            Destroy(go);
    Debug.Log("25 result.Count = " + result.Count);
        }
        GOs.Clear();
    Debug.Log("Dice cleared. Result: " + outcome[0] + " Damage, " + outcome[1] + " Block, " + outcome[2] + " Healing");
        return result;
    }

    public GameObject unpackDie(DieStats packed, Vector3 pos)
    {
        GameObject go = Instantiate(diePrefabs[packed._numSides - 3], pos, Quaternion.identity);
        GOs.Add(go);
        Debug.Log("GOs.Count = " + GOs.Count);
        DieScript ds = go.GetComponent(typeof(DieScript)) as DieScript;
        ds.init(packed);
        return go;
    }
}

