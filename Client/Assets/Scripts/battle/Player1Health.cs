using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Health : MonoBehaviour
{
    public static float health1percent = 1;
    private Image healthbar1;
    void Start()
    {
        healthbar1 = GetComponent<Image>();
        healthbar1.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar1.fillAmount = health1percent;
    }
}
