using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string name;
    public int maxHealth, curHealth;
    public int attackStrength;

    public Enemy(string _name, int hp, int strength){ //oh hey I love that band
        name = _name;
        maxHealth = curHealth = hp;
        attackStrength = strength;
    }

    public void changeHealth(int n){
        curHealth += n;
        if(curHealth > maxHealth){
            curHealth = maxHealth;
        }
    }
}