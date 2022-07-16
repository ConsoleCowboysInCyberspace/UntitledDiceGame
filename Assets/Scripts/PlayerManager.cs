using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// higher level manager for player actions and subcomponents
public class PlayerManager : MonoBehaviour
{
    public DieDeck _dieDeck;
    public List<DieStats2> _dieHand;
    public DieUnpacker _dieUnpacker;

    public Vector3 spawnPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _dieDeck = new DieDeck();
        _dieHand = new List<DieStats2>();
        spawnPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            drawDie();
        }
    }

    public void resolveTurn(){}

    // actions
    public void drawDie()
    {
        DieStats2 pdie = _dieDeck.drawDie();
        if(pdie != null){
            GameObject godie = _dieUnpacker.unpackDie(pdie, spawnPos);
            _dieHand.Add(pdie);
        } else {
            Debug.Log("out of dice!");
        }
    }
}
