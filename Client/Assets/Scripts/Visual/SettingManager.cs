using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool hideSetting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hideSetting)
        {
            gameObject.SetActive(false);
            hideSetting = false;
        }
    }

    public void OnExitButtonClicked()
    {
        hideSetting = true;
    }

    public void OnQuitGameClicked()
    {
        Application.Quit();
    }
}
