using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public InputField InputField;
    public Dropdown DropdownSelecter;
    public RectTransform Buttons;
    public RectTransform InputAndSelect;
    public GameObject SettingScene;
    public GameObject MenuScene;
    public GameObject BattleScene;
    public Text EnterButtonText;

    private GameObject toSetActive = null;
    private GameObject toDeactive = null;
    private void Awake()
    {
        BattleScene.SetActive(true);
        BattleScene.SetActive(false);
    }
    private void Update()
    {
        if(toSetActive != null)
        {
            toSetActive.gameObject.SetActive(true) ;
            toSetActive = null;
        }
    }
    public void PlayGame(){
        StartCoroutine(TranslateButtonsSmoothly(200));
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void Settings(){
        SettingManager.SettingManagerInstance.ShowSetting(MenuScene);
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
        EnterButtonText.text = "等待另一位玩家进入中";
        yield return new WaitUntil(() => PlayerManager.isRivalPlayerSet == true);
        BattleScene.SetActive(true);
        MenuScene.SetActive(false);
    }
}
