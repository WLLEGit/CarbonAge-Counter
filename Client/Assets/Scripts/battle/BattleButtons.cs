using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButtons : MonoBehaviour
{
    public static BattleButtons BattleButtonsInstance;
    public Button CardsButton;
    public GameObject SettingScene;
    public GameObject SelectingCardSectionScene;
    private void Start()
    {
       BattleButtonsInstance = this;
    }
    // Start is called before the first frame update
    public void Settings(){
        SettingScene = GameObject.Find("SettingScene");
        SettingScene.SetActive(true);
    }
    public void CanUse(){
            //CardsButton.enabled = true;
            //CardsButton.interactable = true;
    }
    public void Cards(){
        SelectingCardSectionScene = GameObject.Find("SelectingCardsInterface");
        SelectingCardSectionScene.SetActive(true);
    }
    public void Finish(){
            CardsButton.enabled = false;
            CardsButton.interactable = false;
    }
}
