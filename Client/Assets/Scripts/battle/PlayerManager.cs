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
    // Start is called before the first frame update
    public void Player1(double era, double mili, double carbon)
    {
        string eratext = era.ToString();
        string militext = mili.ToString();
        string cartext = carbon.ToString();
        Player1MilText.text = eratext;
        Player1EraText.text = militext;
        Player1CarText.text = cartext;
    }

    // Update is called once per frame
    public void Player2(double era, double mili, double carbon)
    {
        string eratext = era.ToString();
        string militext = mili.ToString();
        string cartext = carbon.ToString();
        Player2MilText.text = eratext;
        Player2EraText.text = militext;
        Player2CarText.text = cartext;
    }
}
