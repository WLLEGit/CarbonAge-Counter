using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEngine.UI;

public class battle : MonoBehaviour
{
    public static battle BattleInstance;
    public static string InnerGamePath = "./CarbonAge-Counter/Game/app.exe";
    public GameObject WinLabel;
    public GameObject LoseLabel;
    public GameObject DieLabel;

    public Sprite[] Backgrounds;
    public Image Background;
    private List<GameObject> toSetActive = new List<GameObject>();
    void Awake()
    {
        BattleInstance = this;
    }
    private void Update()
    {
        
        if(toSetActive.Count != 0)
        {
            foreach(var obj in toSetActive)
                obj.gameObject.SetActive(true);
        }
        toSetActive = new List<GameObject>();
        if (ClientNetwork.ClientNetworkInstance.CurrentTime == TimeClasses.Time1)
            Background.sprite = Backgrounds[0];
        else if (ClientNetwork.ClientNetworkInstance.CurrentTime == TimeClasses.Time2)
            Background.sprite = Backgrounds[1];
        else if (ClientNetwork.ClientNetworkInstance.CurrentTime == TimeClasses.Time3)
            Background.sprite = Backgrounds[2];
    }
    public void Win()
    {
        toSetActive.Add(WinLabel);
    }
    public void Die()
    {
        toSetActive.Add(DieLabel);
    }
    public void Lose()
    {
        toSetActive.Add(LoseLabel);
    }
    public void StartTurn()
    {
        BattleButtons.isCardsButtonEnabled = true;
    }
    public void StartTransitionGame()
    {
        //Process.Start(InnerGamePath);
    }
}
