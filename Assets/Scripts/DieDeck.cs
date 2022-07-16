using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieDeck
{
    List<DieStats2> remaining;
    List<DieStats2> discard;

    public DieDeck(){
        remaining = new List<DieStats2>();
        discard = new List<DieStats2>();
        
        int sidesMod = 0;
        for(int i = 0; i < 8; i++){
            DieStats2 ds = new DieStats2("arg", 3 + (sidesMod++));
            sidesMod %= 4;
            remaining.Add(ds);
        }
        if(remaining[0] == null) { Debug.Log("Hrm"); }

        Shuffle(remaining);
    }

    public void Discard(List<DieStats2> cleared){
        discard.AddRange(cleared);
    }

    public DieStats2 drawDie()
    {
        if(remaining.Count != 0){
            DieStats2 toDraw = remaining[0];
            remaining.RemoveAt(0);
            Debug.Log("Removing one die, die remaining = " + remaining.Count);
            return toDraw;
        } else if (discard.Count != 0) {
            //Shuffle discard back into 
            remaining.AddRange(discard);
            discard.Clear();
            Shuffle(remaining);

            DieStats2 toDraw = remaining[remaining.Count - 1];
            remaining.RemoveAt(remaining.Count - 1);
            return toDraw;
        } else {
            return null;
        }
    }

    public void Shuffle(List<DieStats2> alpha)  
    {  
        for (int i = 0; i < alpha.Count; i++) {
            DieStats2 temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
    }
}
