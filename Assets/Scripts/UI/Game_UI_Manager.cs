using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game_UI_Manager : MonoBehaviour
{
    VisualElement root;
    Label log;
    ScrollView scroll;
    Label playerInfo;
    Label enemyInfo;
    Button endTurnButton;
    Button menu;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        log = root.Q<Label>("log");
        scroll = root.Q<ScrollView>("log_scroll_container");
        playerInfo = root.Q<Label>("player_info");
        enemyInfo = root.Q<Label>("enemy_info");
        endTurnButton = root.Q<Button>("end_turn_button");
        menu = root.Q<Button>("menu_button");

        var test = root.Q<Button>("test_button");

        //test.RegisterCallback<ClickEvent>(e => buttonCallback());
    }

    public void writeLog(string str)
    {
        log.text = log.text + str;
    }

    public void writeLogLn(string str)
    {
        log.text = log.text + str + "\n";
    }

    public void setPlayerInfo(string str)
    {
        playerInfo.text = str;
    }

    public void setEnemyInfo(string str)
    {
        enemyInfo.text = str;
    }
}
