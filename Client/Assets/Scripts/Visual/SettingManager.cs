using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager SettingManagerInstance;
    public Canvas Canvas;
    bool hideSetting = false;
    bool showSetting = false;
    GameObject returnGameObject = null;
    void Start()
    {
        SettingManagerInstance = this;
        Canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(hideSetting)
        {
            Canvas.gameObject.SetActive(false);
            hideSetting = false;
            returnGameObject.SetActive(true);
        }
        if(showSetting)
        {
            returnGameObject.SetActive(false);
            Canvas.gameObject.SetActive(true);
            showSetting = false;
        }
    }

    public void ShowSetting(GameObject gameObject)
    {
        returnGameObject = gameObject;
        showSetting = true;
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("ExitSetting");
        hideSetting = true;
    }

    public void OnQuitGameClicked()
    {
        Application.Quit();
    }
}
