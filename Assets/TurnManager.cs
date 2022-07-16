using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private PlayerManager enemy;

    public void resolveTurn()
    {
        player.resolveTurn();
        enemy.resolveTurn();
    }
}
