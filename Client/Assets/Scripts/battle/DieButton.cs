using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieButton : MonoBehaviour
{    public GameObject MenuScene;
    public GameObject BattleScene;
    public GameObject Die;
    public void Return()
    {
        MenuScene.SetActive(true);
        BattleScene.SetActive(false);
        Die.SetActive(false);
    }
}
