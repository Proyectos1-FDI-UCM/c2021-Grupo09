using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHovering : MonoBehaviour
{
    public GameObject enableWhenHovering;
    void OnMouseOver()
    {
        if (Time.timeScale != 0f) {
            //If your mouse hovers over the GameObject with the script attached, output this message
            enableWhenHovering.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if(Time.timeScale != 0f)
        {
           //The mouse is no longer hovering over the GameObject so output this message each frame
           enableWhenHovering.SetActive(false);
        }  
    }
}
