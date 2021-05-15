using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;

public class Battle : MonoBehaviour
{
    public static Battle BattleInstance;
    public static string outputpath = @"./CarbonAge-Counter/Game/app.exe";
    public GameObject win;
    public GameObject lose;
    public GameObject die;
    void Start()
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
    public void StartTurn() //����CardsButton����ʾ��ʼ������
    {
        BattleButtons.ButtonButtonsInstance.CanUse();
    }
    public void StartTransitionGame()   //��������С��Ϸ
    {
        Process.Start(outputpath);
    }
}
