using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClicked()
    {
        gameObject.SetActive(false);
        SpeicalEventManager.SpeicalEventManagerInstance.ReturnToBattleScene();
    }
}
