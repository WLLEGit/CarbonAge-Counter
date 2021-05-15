using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Health : MonoBehaviour
{
    public static float health2percent = 1;
    private Image healthbar2;
    void Start()
    {
        healthbar2 = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar2.fillAmount = health2percent;
    }
}
