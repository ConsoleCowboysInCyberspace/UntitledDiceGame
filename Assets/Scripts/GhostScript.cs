using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public List<GameObject> curOverlapping;

    public void Start(){
        curOverlapping = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == null) return;
        curOverlapping.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject == null) return;
        curOverlapping.RemoveAt(curOverlapping.IndexOf(other.gameObject));
    }
}
