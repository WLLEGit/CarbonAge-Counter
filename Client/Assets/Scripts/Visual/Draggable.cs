using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    private bool isMoving = false;
    private void OnMouseDown()
    {
        isMoving = true;
        Debug.Log("OnMouseDown!!");
    }
    private void OnMouseUp()
    {
        isMoving = false;
        Vector3 vector3 = SlotsManager.ManagerSlots.TheNearestSlotVector();
        if (vector3.x == 0 && vector3.y == 0)
            ReturnToPreviousPosition();
        else
        {
            Quaternion quaternion = new Quaternion();
            quaternion.Set(0, 0, 0, 0);
            transform.position = vector3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Input.mousePosition;
        }
    }

    private void ReturnToPreviousPosition()
    {

    }
}
