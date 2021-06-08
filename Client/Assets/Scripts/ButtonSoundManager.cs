using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip MouseEnterSound;
    public AudioClip MousePressSound;
    public AudioClip MouseReleaseSound;
    public AudioClip MouseLeaveSound;

    public AudioSource Sound;

    void Update()
    {
        Sound.loop = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Sound.clip = MouseEnterSound;
        //Sound.Play();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Sound.clip = MousePressSound;
        Sound.Play();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Sound.clip = MouseReleaseSound;
        Sound.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Sound.clip = MouseLeaveSound;
        Sound.Play();
    }

}
