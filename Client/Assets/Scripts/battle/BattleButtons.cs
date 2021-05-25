using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButtons : MonoBehaviour
{
    public static BattleButtons BattleButtonsInstance;
    public Button CardsButton;
    public Button AttackButton;
    public GameObject SettingScene;
    public GameObject SelectingCardSectionScene;
    public GameObject BattleScene;

    public static bool isCardsButtonEnabled = false;
    private void Start()
    {
        BattleButtonsInstance = this;
        CardsButton.enabled = false;
        CardsButton.interactable = false;
        AttackButton.enabled = false;
        AttackButton.interactable = false;
    }
    private void Update()
    {
        CanUse();
        if (isCardsButtonEnabled)
        {
            CanUse();
            isCardsButtonEnabled = false;
        }
    }
    // Start is called before the first frame update
    public void Settings()
    {
        SettingManager.SettingManagerInstance.ShowSetting(BattleScene.gameObject);
    }
    public void CanUse()
    {
        Debug.Log("CanUse");
        CardsButton.enabled = true;
        CardsButton.interactable = true;
        AttackButton.enabled = true;
        AttackButton.interactable = true;
    }
    public void Cards()
    {
        SelectingCardSectionScene.SetActive(true);
        BattleScene.SetActive(false);
    }
    public void Finish()
    {
        Debug.Log("Finish");
        CardsButton.enabled = false;
        CardsButton.interactable = false;
        AttackButton.enabled = false;
        AttackButton.interactable = false;
        ClientNetwork.ClientNetworkInstance.EndThisTurn();
    }
    public void Attack()
    {
        ClientNetwork.ClientNetworkInstance.DealDamage();
    }
}
