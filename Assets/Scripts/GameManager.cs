using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager player;
    public Enemy curEnemy;
    List<Enemy> remainingEnemies;
    public DieUnpacker _dieUnpacker;
    Vector3 spawnPos;

    void Start()
    {
        spawnPos = gameObject.transform.position;
        remainingEnemies = new List<Enemy>();
        remainingEnemies.Add(new Enemy("Slime", 10, 3));
        remainingEnemies.Add(new Enemy("Giga Slime", 20, 5));
        spawnEnemy();
    }

    void Update()
    {
        if(player.alive){
            if (Input.GetKeyDown("space")) {
                spawnDie();
            }
            if (Input.GetKeyDown(KeyCode.Return)) {
                endTurn();
            }
        }
    }

    public void spawnDie()
    {
        if(player._dieHand.Count != 0){
            DieStats pdie = player._dieHand[0];
            if(pdie != null){
                GameObject godie = _dieUnpacker.unpackDie(pdie, spawnPos);
            }
            player._dieHand.RemoveAt(0);
        } else {
            Debug.Log("No dice in hand!");
        }
    }

    public void endTurn(){
        int[] results = _dieUnpacker.readDice();
        int damageTaken = 0;
        if(curEnemy != null){
            //Heal
            player.changeHealth(results[2]); 
            //Attack
            curEnemy.changeHealth(-(results[0]));
            //Take unblocked damage
            if(curEnemy.curHealth > 0){
                damageTaken = curEnemy.attackStrength - results[1]; 
                if(damageTaken < 0) { damageTaken = 0; }
                player.changeHealth(-damageTaken);
            }
        }

        //Clear the dice and put them back in the discard
        List<DieStats> cleared = _dieUnpacker.clearDice();
        player._dieDeck.Discard(cleared);
        player._dieDeck.Discard(player._dieHand);
        
        //Output log messages and check state
        Debug.Log("Turn ended. You attack the " + curEnemy.name + " for " + results[0] + " damage, heal " + results[2] + " hp, then take " + damageTaken + " damage");
        if(player.curHealth <= 0){
            gameOver();
        } else if(curEnemy.curHealth <= 0){
            defeatEnemy();
        } else {
            Debug.Log("You have " + player.curHealth + "hp, the " + curEnemy.name + " has " + curEnemy.curHealth + " hp");
        }

        //Draw back up to 5
        for(int i = 0; i < 5; i++){
            player.drawDie();
        }
    }

    public void gameOver(){
        player.alive = false;
        Debug.Log("You die :(");
    }

    public void spawnEnemy(){
        curEnemy = remainingEnemies[0];
        remainingEnemies.RemoveAt(0);
        Debug.Log("A " + curEnemy.name + " approaches");
    }

    public void defeatEnemy(){
        Debug.Log("The " + curEnemy.name + " is defeated!");
        if(remainingEnemies.Count == 0){
            Debug.Log("You've defeated all the enemies!");
            player.alive = false; //Probably change this later
        } else {
            spawnEnemy();
        }
    } 
}
