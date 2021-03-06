using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Transform Player1ScoreBar;
    public Transform Player2ScoreBar;
    public Transform Player1CarbonBar;
    public Transform Player2CarbonBar;

    public static PlayerManager PlayerManagerInstance;
    public static bool isRivalPlayerSet = false;

    [HideInInspector]
    public List<double> arg1, arg2, arg3;
    public List<Action<double, double, double>> Actions = new List<Action<double, double, double>>();
    const int MAXCARBON = 850;
    private void Awake()
    {
        PlayerManagerInstance = this;
    }
    private void Update()
    {
        while(Actions.Count != 0)
        {
            Actions[0](arg1[0], arg2[0], arg3[0]);
            Actions.RemoveAt(0);
            arg1.RemoveAt(0);
            arg2.RemoveAt(0);
            arg3.RemoveAt(0);
        }
    }
    // Start is called before the first frame update
    public void Player1SetPoints(double era, double mili, double carbon)
    {
        Player1ScoreBar.GetComponent<ScoreBarManager>().SetPoints(mili + 2 * era - 3 * carbon, era, mili);
        Player1CarbonBar.GetComponent<CarbonProgressBarManager>().SetCarbonPercent(carbon / MAXCARBON);
    }
    public void Player1SetDeltaPoints(double deltaEra, double deltaMili, double deltaCarbon)
    {
        Player1ScoreBar.GetComponent<ScoreBarManager>().SetDeltaPoints(deltaMili + 2 * deltaEra - 3 * deltaCarbon, deltaEra, deltaMili);
    }

    // Update is called once per frame
    public void Player2SetPoints(double era, double mili, double carbon)
    {
        Player2ScoreBar.GetComponent<ScoreBarManager>().SetPoints(mili + 2 * era - 3 * carbon, era, mili);
        Player2CarbonBar.GetComponent<CarbonProgressBarManager>().SetCarbonPercent(carbon / MAXCARBON);
    }
    public void Player2SetDeltaPoints(double deltaEra, double deltaMili, double deltaCarbon)
    {
        Debug.Log(string.Format("Player2SetDeltaPoints, ARGS: {0} {1} {2}",deltaEra, deltaMili, deltaCarbon));
        Player2ScoreBar.GetComponent<ScoreBarManager>().SetDeltaPoints(deltaMili + 2 * deltaEra - 3 * deltaCarbon, deltaEra, deltaMili);
    }

    public void Player2SetLeader(Leaders leaderType)
    {
        isRivalPlayerSet = true;
    }
    public void Player1BeenAttacked(int amount)
    {

    }
}
