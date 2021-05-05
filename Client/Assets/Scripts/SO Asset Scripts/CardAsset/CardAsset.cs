using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TimeClasses{Time1,Time2,Time3};

public class CardAsset : ScriptableObject 
{
    // this object will hold the info about the most general card
    [Header("Police info")]
    [TextArea(2,3)]
    public string Description;  
    public string name;
	public int Tec;
    public int Mil;
    public int Carbon;
    public TimeClasses Time;
}
