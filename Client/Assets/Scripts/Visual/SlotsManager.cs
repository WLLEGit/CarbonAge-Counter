using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    public static bool isMouseHovered=false;
    public static int whichIsHovered;
    public Transform[] Slots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30f);

        foreach (RaycastHit h in hits)
        {
            for(int i = 0;i < Slots.Length; ++i)
                if(h.collider == Slots[i].GetComponent<BoxCollider>())
                {
                    whichIsHovered = i;
                    break;
                }
        }
    }
}
