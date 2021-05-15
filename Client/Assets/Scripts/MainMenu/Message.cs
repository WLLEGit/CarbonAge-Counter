using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Message : MonoBehaviour
{
    // Start is called before the first frame update
     public void Click()
    {
        gameObject.SetActive(true); 
    }
    public void Exit(){
        SceneManager.LoadScene("Menu");
    }

}
