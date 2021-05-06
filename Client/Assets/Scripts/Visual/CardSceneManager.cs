using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExitButtonClicked()
    {
        if(!SlotsManager.IsFull)
        {
            List<string> cardNames = new List<string>();
            foreach (var card in SlotsManager.SelectedCards)
                cardNames.Add(card.ToString());
            ClientNetwork.ClientNetworkInstance.ChangeCardBoard(cardNames);
        }
        else
        {
            ;
        }
    }
}
