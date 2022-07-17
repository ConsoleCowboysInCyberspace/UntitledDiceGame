using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieUnpacker : MonoBehaviour
{
    public PlayerManager player;
    public GameManager manager;
    public List<GameObject> diePrefabs;
    public List<GameObject> handPool;
    public List<GameObject> playedDice;
    float sinceLastSpawn = 1;

    void Start() {
        handPool = new List<GameObject>();
    }

    void Update(){
        if(sinceLastSpawn > 0.4f && player._dieHand.Count != 0){
            DieStats pdie = player._dieHand[0];
            if(pdie != null){
                GameObject godie = unpackDie(pdie, gameObject.transform.position);
                Rigidbody2D rb = godie.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(1, -5), ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse);
            }
            player._dieHand.RemoveAt(0);
            sinceLastSpawn = 0;
        }
        sinceLastSpawn += Time.deltaTime;
        sinceLastSpawn %= 300;
    }

    public void playDie(GameObject die){
        playedDice.Add(die);
        handPool.RemoveAt(handPool.IndexOf(die));
    }

    public List<DieStats> clearDice(){
        List<DieStats> result = new List<DieStats>();
        int[] outcome = new int[(int)EffectType.NUM_EFFECTS];
        foreach (GameObject go in handPool) {
            DieScript dScript = go.GetComponent(typeof(DieScript)) as DieScript;
            result.Add(dScript.stats);
            Destroy(go);
        }
        foreach (GameObject go in playedDice) {
            DieScript dScript = go.GetComponent(typeof(DieScript)) as DieScript;
            result.Add(dScript.stats);
            Destroy(go);
        }
        handPool.Clear();
        playedDice.Clear();
        return result;
    }

    public bool AreDiceMoving(){
        foreach(GameObject go in playedDice){
            DieScript dScript = go.GetComponent(typeof(DieScript)) as DieScript;
            if(dScript.isRotating || dScript.isMoving){
                return true;
            }
        }
        return false;
    }

    public int[] readDice(){
        int[] outcome = new int[(int)EffectType.NUM_EFFECTS];
        foreach (GameObject go in playedDice)
        {
            DieScript dScript = go.GetComponent(typeof(DieScript)) as DieScript;
            int whatEffect = (int)(dScript.curEffect().effectType);
            outcome[whatEffect] += dScript.curEffect().value;
        }
        return outcome;
    }

    public void triggerInstantEffects(){
        foreach (GameObject go in playedDice)
        {
            DieScript dScript = go.GetComponent(typeof(DieScript)) as DieScript;
            int whatEffect = (int)(dScript.curEffect().effectType);
            if(dScript.curEffect().effectType == EffectType.Explode){
                Debug.Log("Kaboom");
            }
        }
    }

    public GameObject unpackDie(DieStats packed, Vector3 pos)
    {
        GameObject go = Instantiate(diePrefabs[packed._numSides - 3], pos, Quaternion.identity);
        handPool.Add(go);
        //Debug.Log("handPool.Count = " + handPool.Count);
        DieScript ds = go.GetComponent(typeof(DieScript)) as DieScript;
        ds.init(packed, this);
        return go;
    }
}

