using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseButton : MonoBehaviour
{    public GameObject MenuScene;
    public GameObject BattleScene;
    public GameObject Lose;
    public void Return()
    {
        MenuScene.SetActive(true);
        BattleScene.SetActive(false);
        Lose.SetActive(false);
    }
}
