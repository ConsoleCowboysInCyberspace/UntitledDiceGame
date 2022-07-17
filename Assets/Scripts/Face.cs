using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    public string name;
    public bool triggered;
    public EffectStruct effect;

    public Face(EffectType type, int val){
        switch(type){
            case(EffectType.Damage):
                name = "Attack"; break;
            case(EffectType.Block): 
                name = "Defend"; break;
            case(EffectType.Heal): 
                name = "Healing"; break;
        }
        triggered = false;
        effect = new EffectStruct(type, val);
    }
}

public struct EffectStruct {
    public EffectType effectType;
    public int value;

    public EffectStruct(EffectType e, int v){
        effectType = e; 
        value = v;
    }
}

public enum EffectType{
    Damage,
    Block,
    Heal,
    Explode,
    NUM_EFFECTS
}