using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeicalEventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform BingDong;
    public Transform DaHaiXiao;
    public Transform DaMianJiTingDian;
    public Transform FengBaoChao;
    public Transform YuanGuBingDu;

    public Transform BattleScene;

    public static SpeicalEventManager SpeicalEventManagerInstance;

    private Transform ToBeActivated = null;
    private Transform ToBeDeactivated = null;
    void Start()
    {
        SpeicalEventManagerInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (ToBeActivated != null)
        {
            ToBeActivated.gameObject.SetActive(true);
            ToBeActivated = null;
        }
        if(ToBeDeactivated != null)
        {
            ToBeDeactivated.gameObject.SetActive(false);
            ToBeDeactivated = null;
        }
    }

    public void ActivateSpecialEvent(SpecialEvents specialEvents)
    {
        ToBeDeactivated = BattleScene;
        switch (specialEvents)
        {
            case SpecialEvents.DefaultSpecialEvent:
                break;
            case SpecialEvents.BingDong:
                ToBeActivated = BingDong;
                break;
            case SpecialEvents.DaHaiXiao:
                ToBeActivated = DaHaiXiao;
                break;
            case SpecialEvents.DaMianJiTingDian:
                ToBeActivated = DaMianJiTingDian;
                break;
            case SpecialEvents.FengBaoChao:
                ToBeActivated = FengBaoChao;
                break;
            case SpecialEvents.YuanGuBingDu:
                ToBeActivated = YuanGuBingDu;
                break;
        }
    }
    public void ReturnToBattleScene()
    {
        ToBeActivated = BattleScene;
    }
}
