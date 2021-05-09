using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleButtons : MonoBehaviour
{
    public static bool useable = true;
    // Start is called before the first frame update
    public void Settings(){
            SceneManager.LoadScene(2);
    }
    public void Cards(){
         if(useable == true){
             this.GetComponent<Button>().enabled = true;
            this.GetComponent<Button>().interactable = true;
            SceneManager.LoadScene(0);
        }
        else{
            this.GetComponent<Button>().enabled = false;
            this.GetComponent<Button>().interactable = false;
        }
    }
    public void Finish(){
        useable = false;
    }
}
