using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUnpacker : MonoBehaviour
{
    public GameObject diePrefab;
    
    public GameObject unpackDie(PackedDie packed, Vector3 pos)
    {
        // add packed effects to the die here
        return Instantiate(diePrefab, pos, Quaternion.identity);
    }
}

