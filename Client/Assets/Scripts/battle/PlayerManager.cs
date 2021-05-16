using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text Player1MilText;
    public Text Player1EraText;
    public Text Player1CarText;
    public Text Player2MilText;
    public Text Player2EraText;
    public Text Player2CarText;
    public static PlayerManager PlayerManagerInstance;
    public static bool isRivalPlayerSet = false;

    private void Awake()
    {
        PlayerManagerInstance = this;
    }
    // Start is called before the first frame update
    public void Player1SetPoints(double era, double mili, double carbon)
    {
        string eratext = era.ToString();
        string militext = mili.ToString();
        string cartext = carbon.ToString();
        Player1MilText.text = eratext;
        Player1EraText.text = militext;
        Player1CarText.text = cartext;
    }

    // Update is called once per frame
    public void Player2SetPoints(double era, double mili, double carbon)
    {
        string eratext = era.ToString();
        string militext = mili.ToString();
        string cartext = carbon.ToString();
        Player2MilText.text = eratext;
        Player2EraText.text = militext;
        Player2CarText.text = cartext;
    }

    public void Player2SetLeader(Leaders leaderType)
    {
        isRivalPlayerSet = true;
    }
    public void Player1BeenAttacked(int amount)
    {

    }
}
