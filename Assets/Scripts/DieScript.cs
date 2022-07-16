using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    public DieStats stats;
    List<List<(float a, float b)>> ranges;

    // Start is called before the first frame update
    void Start()
    {
        ranges = new List<List<(float a, float b)>>();
        ranges.Add(new List<(float a, float b)>{ (-0.1f, 120.0f), (120.0f, 240.0f), (240.0f, 360.1f) }); //Three sides 
        ranges.Add(new List<(float a, float b)>{ (44.9f, 315.0f), (45.0f, 135.0f), (135.0f, 225.0f), (225.0f, 315.1f) }); //Four sides
        ranges.Add(new List<(float a, float b)>{ (-0.1f, 72.0f), (72.0f, 144.0f), (144.0f, 216.0f), (216.0f, 288.0f), (288.0f, 360.1f) }); //Five sides
        ranges.Add(new List<(float a, float b)>{ (29.9f, 330.0f), (30.0f, 90.0f) , (90.0f, 150.0f) , (150.0f, 210.0f), (210.0f, 270.0f), (270.0f, 330.1f) }); //Six sides
    }

    public void init(DieStats _stats){
        stats = _stats;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Curside = " + curSide());
    }

    int curSide(){ //return Side when ready
        float curRot = transform.localRotation.eulerAngles.z;
        curRot %= 360;
        List<(float a, float b)> rangeSet = ranges[stats._numSides - 3];
        bool isEven = stats._numSides % 2 == 0;
//Debug.Log("CurRot = " + (int)curRot + "  rangeSet[0] = (" + rangeSet[0].a + "," + rangeSet[0].b + ")   isEven = " + isEven);
        if(isEven && (curRot <= rangeSet[0].a || curRot > rangeSet[0].b)){
            return 0;
        }
        int start = (isEven ? 1 : 0);
        for(int i = start; i < rangeSet.Count; i++){
            (float a, float b) range = rangeSet[i];
            if (curRot > range.a && curRot <= range.b){
                return i;
            }
        }
        return -1;
    }
}