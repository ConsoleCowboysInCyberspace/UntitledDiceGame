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
        ranges.Add(new List<(float a, float b)>{ (60.0f, 300.0f), (60.0f, 180.0f), (180.0f, 300.0f) }); //Three sides 
        ranges.Add(new List<(float a, float b)>{ (45.0f, 315.0f), (45.0f, 135.0f), (135.0f, 225.0f), (225.0f, 315.1f) }); //Four sides
        ranges.Add(new List<(float a, float b)>{ (36.0f, 324.0f), (36.0f, 108.0f), (108.0f, 180.0f), (180.0f, 252.0f), (252.0f, 324.1f) }); //Five sides
        ranges.Add(new List<(float a, float b)>{ (30.0f, 330.0f), (30.0f, 90.0f) , (90.0f, 150.0f) , (150.0f, 210.0f), (210.0f, 270.0f), (270.0f, 330.1f) }); //Six sides
    }

    public void init(DieStats _stats){
        stats = _stats;
    }

    // Update is called once per frame
    void Update()
    {
        if(stats._numSides == 3 || stats._numSides == 6){
            //Debug.Log("Curside = " + curSide());
        }
    }

    public EffectStruct curEffect(){
        //Debug.Log("Faces.count = " + stats._faces.Count);
        return (stats._faces[curSide()].effect);
    }

    int curSide(){ //return Side when ready
        float curRot = transform.localRotation.eulerAngles.z;
        curRot %= 360;
        List<(float a, float b)> rangeSet = ranges[stats._numSides - 3];
        bool isOdd = stats._numSides % 2 == 1;
//Debug.Log("CurRot = " + (int)curRot + "  rangeSet[0] = (" + rangeSet[0].a + "," + rangeSet[0].b + ")   isOdd = " + isOdd);
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