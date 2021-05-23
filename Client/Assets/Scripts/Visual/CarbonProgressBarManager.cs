using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarbonProgressBarManager : MonoBehaviour
{
    public Image ProgressBar;
    public Image PlayerImage;

    public Sprite[] CarbonBarSprites;
    // Start is called before the first frame update
    void Start()
    {
        ProgressBar.fillAmount = (float)0;
        //StartCoroutine(ChangeCarbonBarSprite());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SetCarbonPercent(double amount)
    {
        Debug.Log("SetPercent Called, " + amount.ToString());
        ProgressBar.fillAmount = (float)amount;
    }
    private IEnumerator ChangeCarbonBarSprite()
    {
        while(true)
        {
            foreach(var sprite in CarbonBarSprites)
            {
                ProgressBar.sprite = sprite;
                yield return new WaitForSeconds((float)0.1);
            }
        }
    }
}
