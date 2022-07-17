using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// player's hand of dice
// exists physically in the world

public class DieHand : MonoBehaviour
{
    public PlayerManager player;
    public List<GameObject> diceInHand;
    public Vector3 spawnPos;
    
    void Start()
    {
        spawnPos = gameObject.transform.position;
    }

    void Update()
    {
        
    }
}
