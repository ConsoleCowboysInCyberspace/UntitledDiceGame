using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieDeck
{
    List<DieStats> remaining;
    List<DieStats> discard;

    public DieDeck(){
        remaining = new List<DieStats>();
        discard = new List<DieStats>();
        
        int sidesMod = 0;
        for(int i = 0; i < 8; i++){
            string type = "";
            switch(sidesMod){
                case(0): { type = "Heal"; break; }
                case(1): { type = "Defend"; break; }
                case(2): { type = "Attack"; break; }
                case(3): { type = "Attack"; break; }
                default: { Debug.Log("invalid sidesMod");  break; }
            }
            DieStats ds = new DieStats("arg", 3 + (sidesMod++), type);
            sidesMod %= 4;
            remaining.Add(ds);
        }
        if(remaining[0] == null) { Debug.Log("Hrm"); }

        Shuffle(remaining);
    }

    public void Discard(List<DieStats> cleared){
        discard.AddRange(cleared);
    }

    public DieStats drawDie()
    {
        if(remaining.Count != 0){
            DieStats toDraw = remaining[0];
            remaining.RemoveAt(0);
            //Debug.Log("Drawing a die - " + remaining.Count + " dice left in the deck");
            return toDraw;

        } else if (discard.Count != 0) {
            //Shuffle discard back into remaining
            remaining.AddRange(discard);
            discard.Clear();
            Shuffle(remaining);

            DieStats toDraw = remaining[remaining.Count - 1];
            remaining.RemoveAt(remaining.Count - 1);
            return toDraw;
        } else {
            return null;
        }
    }

    //sourced from StackOverflow
    public void Shuffle(List<DieStats> alpha)  
    {  
        for (int i = 0; i < alpha.Count; i++) {
            DieStats temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
    }
}
