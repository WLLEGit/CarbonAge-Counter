using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour
{
    public CardAsset CardAsset;
    public bool FitTime;
    public TimeClasses c_time;
    [Header("Text Component References")]
    public Text Title_Text;
    public Text Mil_Text;
    public Text Description_Text;
    public Text Tec_Text;
    public Text Carbon_Text;
    public TimeClasses Time;
    public Vector3 Position;

    public Cards CardType;

    void JudgeTime()
    {
        c_time=ClientNetwork.ClientNetworkInstance.CurrentTime;
        if(c_time==TimeClasses.Time1)
        {
            if(Time==TimeClasses.Time1)
            {
                FitTime=true;
            }
            else
            {
                FitTime=false;
            }
        }
        else if (c_time==TimeClasses.Time2)
        {
            if(Time==TimeClasses.Time1||Time==TimeClasses.Time2)
            {
                FitTime=true;
            }
            else
            {
                FitTime=false;
            }
        }
        else if(c_time==TimeClasses.Time3)
        {
            if(Time==TimeClasses.Time1||Time==TimeClasses.Time2||Time==TimeClasses.Time3)
            {
                FitTime=true;
            }
            else
            {
                FitTime=false;
            }
        }
    }
    void Awake()
    {
        if (CardAsset != null)
            {
                ReadCardFromAsset();
                JudgeTime();
            }
        CardType = (Cards)Enum.Parse(typeof(Cards), CardAsset.Name);
    }
    void Rotation()
    {
        if(!FitTime)
        {
            transform.rotation=Quaternion.Euler(new Vector3(0,180,0));
        }
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

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
        JudgeTime();
        Rotation();    
    }
}
