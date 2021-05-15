using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour
{
    public CardAsset CardAsset;
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
        if (CardAsset != null)
            ReadCardFromAsset();
        CardType = (Cards)Enum.Parse(typeof(Cards), CardAsset.Name);
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
        Title_Text.text = CardAsset.Name;
        Mil_Text.text = CardAsset.Mil.ToString();
        Description_Text.text = CardAsset.Description;
        Tec_Text.text = CardAsset.Tec.ToString();
        Carbon_Text.text = CardAsset.Carbon.ToString();
        Time = CardAsset.Time;
        Position = CardAsset.Position;
    }

    private void Update()
    {

    }
}
