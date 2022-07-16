using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStats
{
    public string _name;
    public int _numSides;
    List<Face> _faces;

    public DieStats(string name, int num){
        _name = name; 
        _numSides = num;
    }
}
 