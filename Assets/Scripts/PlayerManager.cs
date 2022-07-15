using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// higher level manager for player actions and subcomponents
public class PlayerManager : MonoBehaviour
{
    public DieDeck _dieDeck;
    public DieHand _dieHand;
    public DieUnpacker _dieUnpacker;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            drawDie();
        }
    }

    // actions
    public void drawDie()
    {
        PackedDie pdie = _dieDeck.drawDie();
        GameObject godie = _dieUnpacker.unpackDie(pdie, _dieHand.spawnPos);
        _dieHand.diceInHand.Add(godie);
    }
}
