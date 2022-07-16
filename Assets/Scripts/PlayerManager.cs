using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// higher level manager for player actions and subcomponents
public class PlayerManager : MonoBehaviour
{
    public bool alive = true;
    public int maxHealth, curHealth;
    public DieDeck _dieDeck;
    public List<DieStats> _dieHand;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = curHealth = 10;
        _dieDeck = new DieDeck();
        _dieHand = new List<DieStats>();
        for(int i = 0; i < 5; i++){
            drawDie();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // actions
    public void drawDie() {
        DieStats pdie = _dieDeck.drawDie();
        if(pdie != null){
            _dieHand.Add(pdie);
        }
    }

    public void changeHealth(int n){
        curHealth += n;
        if(curHealth > maxHealth){
            curHealth = maxHealth;
        }
    }
}