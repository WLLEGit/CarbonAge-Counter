using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour
{
    public CardAsset cardAsset;
    [Header("Text Component References")]
    public Text Title_Text;
    public Text Mil_Text;
    public Text Description_Text;
    public Text Tec_Text;
    public Text Carbon_Text;
    public TimeClasses Time;
    public Vector3 Position;

    public Cards CardType;

    void Awake()
    {
        if (cardAsset != null)
            ReadCardFromAsset();
        CardType = (Cards)Enum.Parse(typeof(Cards), cardAsset.Name);
    }

    private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            return canBePlayedNow;
        }

        set
        {
            canBePlayedNow = value;
        }
    }

    public void ReadCardFromAsset()
    {
        Title_Text.text = cardAsset.Name;
        Mil_Text.text = cardAsset.Mil.ToString();
        Description_Text.text = cardAsset.Description;
        Tec_Text.text = cardAsset.Tec.ToString();
        Carbon_Text.text = cardAsset.Carbon.ToString();
        Time = cardAsset.Time;
        Position = cardAsset.Position;
    }

    private void Update()
    {

    }
}
