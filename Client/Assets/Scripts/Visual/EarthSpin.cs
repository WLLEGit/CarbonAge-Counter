using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpin : MonoBehaviour
{
    private Vector3 PreMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        PreMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curMousePosition = Input.mousePosition;
        if (curMousePosition.x < PreMousePosition.x)
        {
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        }
        else if (curMousePosition.x > PreMousePosition.x)
        {
            transform.Rotate(-Vector3.up, 200 * Time.deltaTime);
        }
        PreMousePosition = curMousePosition;
    }
}
