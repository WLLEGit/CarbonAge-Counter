using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    public static Battle BattleInstance;
    private void Start()
    {
        BattleInstance = this;
    }
    public void Win()
    {

    }
    public void Die()
    {

    }
    public void Lose()
    {

    }
    public void StartTurn() //启用CardsButton，显示开始弹出框
    {

    }
    public void StartTransitionGame()   //启动过渡小游戏
    {

    }
}
