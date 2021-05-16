using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButtons : MonoBehaviour
{
    public static BattleButtons ButtonButtonsInstance;
    public Button CardsButton;
    public BattleButtons BattleButtonsInstance;
    private void Start()
    {
       BattleButtonsInstance = this;
    }
    // Start is called before the first frame update
    public void Settings(){
        SceneManager.LoadScene("Settings");
    }
    public void CanUse(){
            CardsButton.enabled = true;
            CardsButton.interactable = true;
    }
    public void Cards(){
        SceneManager.LoadScene("SelectingCardsSection");
    }
    public void Finish(){
            CardsButton.enabled = false;
            CardsButton.interactable = false;
    }
}
