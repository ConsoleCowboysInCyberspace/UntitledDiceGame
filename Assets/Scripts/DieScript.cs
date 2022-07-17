using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DieScript : MonoBehaviour
{
    public DieStats stats;
    public bool isInHand, isDragging;
    private GameObject ghost;
    private DieUnpacker unpacker;
    public GameManager manager;
    List<List<(float a, float b)>> ranges;
    private Quaternion lastRot;
    public bool isRotating = false, isMoving = false;

    private Rigidbody2D rb;

    private float dieRadius = 0.561481f;

    // Start is called before the first frame update
    void Start()
    {
        isInHand = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        ranges = new List<List<(float a, float b)>>();
        ranges.Add(new List<(float a, float b)>{ (60.0f, 300.0f), (60.0f, 180.0f), (180.0f, 300.0f) }); //Three sides 
        ranges.Add(new List<(float a, float b)>{ (45.0f, 315.0f), (45.0f, 135.0f), (135.0f, 225.0f), (225.0f, 315.1f) }); //Four sides
        ranges.Add(new List<(float a, float b)>{ (36.0f, 324.0f), (36.0f, 108.0f), (108.0f, 180.0f), (180.0f, 252.0f), (252.0f, 324.1f) }); //Five sides
        ranges.Add(new List<(float a, float b)>{ (30.0f, 330.0f), (30.0f, 90.0f) , (90.0f, 150.0f) , (150.0f, 210.0f), (210.0f, 270.0f), (270.0f, 330.1f) }); //Six sides
    }

    public void init(DieStats _stats, DieUnpacker unp){
        stats = _stats;
        unpacker = unp;
        manager = unpacker.manager;
        lastRot = transform.rotation;
    }

    public void OnMouseDown(){
        if(isInHand && !manager.diceMoving){
            isDragging = true;
            ghost = new GameObject();
            ghost.transform.localScale = transform.localScale;
            ghost.transform.localRotation = transform.localRotation;
            //CopyComponent(gameObject.GetComponent<PolygonCollider2D>(), ghost);
            PolygonCollider2D pColl = ghost.AddComponent<PolygonCollider2D>();
            pColl.SetPath(0, gameObject.GetComponent<PolygonCollider2D>().points);
            pColl.isTrigger = true;
            //ghost.AddComponent<GhostScript>();
            SpriteRenderer sr = ghost.AddComponent<SpriteRenderer>();
            sr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            sr.color = new Color(1f,1f,1f,.5f);
        }
    }

    public void OnMouseUp(){
        isDragging = false;

        if(ghost != null){
            List<Collider2D> curOverlapping = new List<Collider2D>();
            PolygonCollider2D ghostCollider = ghost.GetComponent<PolygonCollider2D>();
            int numOLing = ghostCollider.OverlapCollider(new ContactFilter2D().NoFilter(), curOverlapping);
            if(numOLing == 1 && curOverlapping[0].tag == "Respawn"){
                gameObject.transform.position = ghost.transform.position;
                isInHand = false;
                unpacker.playDie(gameObject);
                rb.AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
                rb.AddTorque(UnityEngine.Random.Range(-2, 2), ForceMode2D.Impulse);
                manager.diceMoving = true;
                manager.playTimer = 0.0f;
            }

            Destroy(ghost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //- transform.position;
            if(ghost != null){ ghost.transform.position = mousePos; }
        }

        if(transform.rotation.y != lastRot.y && Math.Abs(transform.rotation.y-lastRot.y) >= 0.05){  
            isRotating = true;
        } else {
            isRotating = false;
        }

        if(rb.velocity.magnitude >= 0.05){
            isMoving = true;
        } else {
            isMoving = false;
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

    //Source: Shaffe at https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields(); 
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }
}