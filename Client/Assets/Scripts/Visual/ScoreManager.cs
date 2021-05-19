using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Score;
    public Text DeltaScore;
    public Image ArrowImage;

    public Sprite UpArrow;
    public Sprite DownArrow;
    [HideInInspector]
    public bool isIncreasing = true;
    void Start()
    {
        Score.text = "0";
        ArrowImage.sprite = UpArrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIncreasing)
            ArrowImage.sprite = UpArrow;
        else
            ArrowImage.sprite = DownArrow;
    }
}
