using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
   public GameObject AboutUs;
   public void Exit(){
        AboutUs = GameObject.Find("AboutUs");
        AboutUs.SetActive(false); 
    }
}
