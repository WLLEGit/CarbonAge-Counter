using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseButton : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}
