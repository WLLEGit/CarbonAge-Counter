using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBarManager : MonoBehaviour
{
    public Transform Score;
    public Transform EraPoints;
    public Transform MiliPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPoints(double score, double eraPoints, double miliPoints)
    {
        Score.GetComponent<ScoreManager>().Score.text = ((int)score).ToString();
        EraPoints.GetComponent<ScoreManager>().Score.text = ((int)eraPoints).ToString();
        MiliPoints.GetComponent<ScoreManager>().Score.text = ((int)miliPoints).ToString();
    }

    public void SetDeltaPoints(double deltaScore, double deltaEra, double deltaMili)
    {
        Score.GetComponent<ScoreManager>().DeltaScore.text = ((int)deltaScore).ToString();
        Score.GetComponent<ScoreManager>().isIncreasing = (deltaScore >= 0);
        EraPoints.GetComponent<ScoreManager>().DeltaScore.text = ((int)deltaEra).ToString();
        EraPoints.GetComponent<ScoreManager>().isIncreasing = (deltaEra >= 0);
        MiliPoints.GetComponent<ScoreManager>().DeltaScore.text = ((int)deltaMili).ToString();
        MiliPoints.GetComponent<ScoreManager>().isIncreasing = (deltaMili >= 0);
    }
}
