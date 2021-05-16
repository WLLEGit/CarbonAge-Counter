using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEngine.UI;

public class battle : MonoBehaviour
{
    public static battle BattleInstance;
    public static string outputpath = "./CarbonAge-Counter/Game/app.exe";
    public GameObject win;
    public GameObject lose;
    public GameObject die;
    void Awake()
    {
        BattleInstance = this;
    }
    public void Win()
    {
        win = GameObject.Find("Win");
        win.SetActive(true);
    }
    public void Die()
    {
        die = GameObject.Find("Die");
        die.SetActive(true);
    }
    public void Lose()
    {
        lose = GameObject.Find("Lose");
        lose.SetActive(true);
    }
    public void StartTurn()
    {
        BattleButtons.ButtonButtonsInstance.CanUse();
    }
    public void StartTransitionGame()
    {
        Process.Start(outputpath);
    }
}
