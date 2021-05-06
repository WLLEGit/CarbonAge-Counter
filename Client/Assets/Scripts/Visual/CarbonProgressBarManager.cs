using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarbonProgressBarManager : MonoBehaviour
{
    public Image ProgressBar;
    public Image PlayerImage;
    // Start is called before the first frame update
    void Start()
    {
        ProgressBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetCarbonPercent(double amount)
    {
        ProgressBar.fillAmount = (float)amount;
    }
}
