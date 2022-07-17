using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager player;
    public Enemy curEnemy;
    List<Enemy> remainingEnemies;
    public DieUnpacker _dieUnpacker;
    public Game_UI_Manager UIMan;
    Vector3 spawnPos;
    public bool diceMoving;
    public float playTimer = 0.0f;

    void Start()
    {
        player.setMaxHP(10);
        spawnPos = gameObject.transform.position;
        remainingEnemies = new List<Enemy>();
        remainingEnemies.Add(new Enemy("Slime", 10, 3));
        remainingEnemies.Add(new Enemy("Giga Slime", 20, 5));
        UIMan.setPlayerInfo("Damian McDice\nHP: " + player.CurHP() + "/" + player.MaxHP());
        spawnEnemy();
    }

    void Update()
    {
        if(diceMoving && playTimer >= 0.3f){
            diceMoving = _dieUnpacker.AreDiceMoving();
            //Debug.Log("dice are moving");
            if(!diceMoving){
                Debug.Log("Dice have stopped moving");
            }
        }
        if(player.alive){
            if (Input.GetKeyDown("space")) {
                //spawnDie();
            }
            if (Input.GetKeyDown(KeyCode.Return)) {
                endTurn();
            }
        }
        playTimer += Time.deltaTime;
        playTimer %= 500;
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
        UIMan.writeLogLn("Turn ended. You attack the " + curEnemy.name + " for " + results[0] + " damage, heal " + results[2] + " hp, then take " + damageTaken + " damage");
        if(player.CurHP() <= 0){
            gameOver();
        } else if(curEnemy.curHealth <= 0){
            defeatEnemy();
        } 
        UIMan.setPlayerInfo("Damian McDice\nHP: " + player.CurHP() + "/" + player.MaxHP());
        UIMan.setEnemyInfo(curEnemy.name + "\nHP: " + curEnemy.curHealth + "/" +curEnemy.maxHealth + "\nAttack: " + curEnemy.attackStrength);

        //Draw back up to 5
        for(int i = 0; i < 5; i++){
            player.drawDie();
        }
    }

    public void gameOver(){
        player.alive = false;
        UIMan.writeLogLn("You die :(");
    }

    public void spawnEnemy(){
        curEnemy = remainingEnemies[0];
        remainingEnemies.RemoveAt(0);
        UIMan.writeLogLn("A " + curEnemy.name + " approaches!");
        UIMan.setEnemyInfo(curEnemy.name + "\nHP: " + curEnemy.curHealth + "/" +curEnemy.maxHealth + "\nAttack: " + curEnemy.attackStrength);
    }

    public void defeatEnemy(){
        UIMan.writeLogLn("The " + curEnemy.name + " is defeated!");
        if(remainingEnemies.Count == 0){
            UIMan.writeLogLn("You've defeated all the enemies!");
            player.alive = false; //Probably change this later
        } else {
            spawnEnemy();
        }
    } 
}
