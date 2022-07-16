using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStats2 : MonoBehaviour
{
    public string _name;
    public int _numSides;
    List<Face> _faces;

    public DieStats2(string name, int num){
        _name = name; 
        _numSides = num;
    }
}
 