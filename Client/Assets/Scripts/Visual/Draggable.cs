using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    private bool isMoving = false;
    private float zDisplacement;
    private Vector3 pointerDisplacement;

    public static Draggable DraggableInstanse;
    private void OnMouseDown()
    {
        DraggableInstanse = this;
        isMoving = true;
        zDisplacement = -Camera.main.transform.position.z + transform.position.z;
        pointerDisplacement = -transform.position + MouseInWorldCoords();
    }
    private void OnMouseUp()
    {
        isMoving = false;
        Vector3 vector3 = SlotsManager.SlotsManagerInstance.TheNearestSlotVector();
        if (vector3.x == 0 && vector3.y == 0)
        {
            ReturnToPreviousPosition();
            SlotsManager.SlotsManagerInstance.TryRemove(GetComponent<OneCardManager>().CardType);
        }
        else
        {
            transform.position = vector3;
            SlotsManager.SlotsManagerInstance.TryAdd(vector3, GetComponent<OneCardManager>().CardType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Vector3 mousePos = MouseInWorldCoords();
            transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);
        }
    }

    private void ReturnToPreviousPosition()
    {
        transform.Translate(GetComponent<OneCardManager>().Position - transform.position);
    }
    public Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
