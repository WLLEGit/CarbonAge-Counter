using UnityEngine;
using System.Collections;

public enum CharClass{ Leader1,Leader2,Leader3}

public class CharacterAsset : ScriptableObject 
{
	public CharClass Class;
	public string ClassName;
	public int MaxHealth;
	public string HeroPowerName;
	public Sprite LeaderImage;
    public Sprite HeroPowerIconImage;
    public double Tec_up;
    public double Mil_up;
    public double Carbon_up;

}
