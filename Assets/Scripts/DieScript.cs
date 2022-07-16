using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    public string name;
    public int numSides;
    List<List<(float a, float b)>> ranges;
    List<Face> faces;

    // Start is called before the first frame update
    void Start()
    {
        ranges = new List<List<(float a, float b)>>();
        ranges.Add(new List<(float a, float b)>{ (0.0f, 120.0f), (120.0f, 240.0f), (240.0f, 360.0f) }); //Three sides 
        ranges.Add(new List<(float a, float b)>{ (45.0f, 315.0f), (45.0f, 135.0f), (135.0f, 225.0f), (225.0f, 315.0f) }); //Four sides
        ranges.Add(new List<(float a, float b)>{ (0.0f, 72.0f), (72.0f, 144.0f), (144.0f, 216.0f), (216.0f, 288.0f), (288.0f, 360.0f) }); //Five sides
        ranges.Add(new List<(float a, float b)>{ (30.0f, 330.0f), (30.0f, 90.0f) , (90.0f, 150.0f) , (150.0f, 210.0f), (210.0f, 270.0f), (270.0f, 330.0f) }); //Six sides
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Curside = " + curSide());
    }

    int curSide(){ //return Side when ready
        float curRot = transform.localRotation.eulerAngles.z;
        curRot %= 360;
        List<(float a, float b)> rangeSet = ranges[numSides - 3];
        if(curRot <= rangeSet[0].a || curRot > rangeSet[0].b){
            return 0;
        }
        for(int i = 1; i < rangeSet.Count; i++){
            (float a, float b) range = rangeSet[i];
            if (curRot > range.a && curRot <= range.b){
                return i;
            }
        }
        return -1;
    }
}