using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public InputField InputField;
    public Dropdown DropdownSelecter;
    public RectTransform Buttons;
    public RectTransform InputAndSelect;
    public GameObject SettingScene;
    public GameObject MenuScene;
    public GameObject BattleScene;
    private void Awake()
    {
        BattleScene.SetActive(true);
    }
    public void PlayGame(){
        StartCoroutine(TranslateButtonsSmoothly(200));
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void Settings(){
        SettingScene = GameObject.Find("SettingScene");
        SettingScene.SetActive(true);
    }
    public void OnSelectAndInputDone()
    {
        ClientNetwork.ClientNetworkInstance.ConnectServer(InputField.text, 1357);
        ClientNetwork.ClientNetworkInstance.EnterGame(DropdownSelecter.options[DropdownSelecter.value].text);
        StartCoroutine(WaitToStartGame());
    }
    private IEnumerator TranslateButtonsSmoothly(int dis)
    {
        for (int i = 0; i < dis / 3; ++i)
        {
            Buttons.transform.Translate(Vector3.left * 3, Space.World);
            yield return null;
        }
        InputAndSelect.gameObject.SetActive(true);
    }
    private IEnumerator WaitToStartGame()
    {
        EditorUtility.DisplayDialog("WaitAnotherPlayer", "Wait Another Player to Connect....", "OK");
        yield return new WaitUntil(() => PlayerManager.isRivalPlayerSet == true);
        BattleScene.SetActive(true);
        MenuScene.SetActive(false);
    }
}
