using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinButton : MonoBehaviour
{
    public GameObject MenuScene;
    public GameObject BattleScene;
    public GameObject Win;
    public void Return()
    {
        MenuScene.SetActive(true);
        BattleScene.SetActive(false);
        Win.SetActive(false);
    }
}
