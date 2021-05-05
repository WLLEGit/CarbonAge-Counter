using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    public static bool IsFull;
    public static SlotsManager ManagerSlots;
    //public Button CloseButton;

    public Transform[] Slots;
    // Start is called before the first frame update
    void Start()
    {
        ManagerSlots = this;
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsFull)
        //    CloseButton.enabled = true;
        //else
        //    CloseButton.enabled = false;
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

}
