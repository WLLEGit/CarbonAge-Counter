using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public static Message MessageInstance;
    public GameObject AboutUs;
     public void Click()
    {
        AboutUs.SetActive(true); 
    }

}
