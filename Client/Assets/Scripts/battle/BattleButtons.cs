using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButtons : MonoBehaviour
{
    public Button CardsButton;
    public static bool useable = true;
    // Start is called before the first frame update
    public void Settings(){
            SceneManager.LoadScene("Settings");
    }
    public void Cards(){
         if(useable == true){
             CardsButton.enabled = true;
            CardsButton.interactable = true;
            SceneManager.LoadScene("SelectingCardsSection");
        }
        else{
            CardsButton.enabled = false;
            CardsButton.interactable = false;
        }
    }
    public void Finish(){
        useable = false;
    }
}
