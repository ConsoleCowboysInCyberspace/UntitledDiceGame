using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStats
{
    public string _name;
    public int _numSides;
    public List<Face> _faces;

    public DieStats(string name, int num, string type){
        _name = name; 
        _numSides = num;
        _faces = new List<Face>();

        EffectType t = EffectType.NUM_EFFECTS;
        switch(type){
            case("Attack"):
                t = EffectType.Damage; break;
            case("Defend"): 
                t = EffectType.Block; break;
            case("Heal"): 
                t = EffectType.Heal; break;
        }

        for(int i = 0; i < _numSides; i++){
            Face f = new Face(t, i+1);
            _faces.Add(f);
        }
    }
}
 