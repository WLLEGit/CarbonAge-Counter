using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    public static bool IsFull;
    public static SlotsManager SlotsManagerInstance;
    public Button CloseButton;

    public Transform[] Slots;
    public static Cards[] SelectedCards;


    // Start is called before the first frame update
    void Start()
    {
        SlotsManagerInstance = this;
        SelectedCards = new Cards[Slots.Length];
        for (int i = 0; i < SelectedCards.Length; ++i)
            SelectedCards[i] = Cards.DefaultCard;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFull)
            CloseButton.gameObject.SetActive(true);
        else
            CloseButton.gameObject.SetActive(false);
    }

    public Vector3 TheNearestSlotVector()
    {
        Vector3 resVec = new Vector3(0, 0, 0);

        foreach (var slot in Slots)
        {
            Vector3 mousePos = Draggable.DraggableInstanse.MouseInWorldCoords();
            RectTransform rect = (RectTransform)slot.Find("Canvas");
            Vector3 pos = slot.position;
            if (mousePos.x <= pos.x + rect.rect.width / 2 && mousePos.x >= pos.x - rect.rect.width / 2 &&
                 mousePos.y <= pos.y + rect.rect.height / 2 && mousePos.y >= pos.y - rect.rect.height / 2)
                return pos;
        }
        return resVec;
    }

    public void TryRemove(Cards card)
    {
        for (int i = 0; i < SelectedCards.Length; ++i)
            if (SelectedCards[i] == card)
            {
                SelectedCards[i] = Cards.DefaultCard;
                IsFull = false;
            }
    }

    public void TryAdd(Vector3 position, Cards cardType)
    {
        for (int i = 0; i < Slots.Length; ++i)
        {
            if (System.Math.Abs(Slots[i].position.x - position.x) < 2 && System.Math.Abs(Slots[i].position.y - position.y) < 2)
            {
                SelectedCards[i] = cardType;
                break;
            }
        }

        IsFull = true;
        if(ClientNetwork.ClientNetworkInstance.CurrentTime==TimeClasses.Time1)
        {
            if(SelectedCards[0]==Cards.DefaultCard||SelectedCards[1]==Cards.DefaultCard)
            {
                IsFull=false;
            }
        }
        else
        { 
        foreach (var card in SelectedCards)
            if (card == Cards.DefaultCard)
                IsFull = false;
        }
    }

}
