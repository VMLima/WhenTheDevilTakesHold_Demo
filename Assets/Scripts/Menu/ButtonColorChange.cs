using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

    public Text theText;
    public Color color;

    public void OnPointerEnter(PointerEventData eventData) {
        theText.color = Color.red; 
    }

    public void OnPointerExit(PointerEventData eventData) {
        theText.color = Color.white; 
    }

    public void OnPointerDown(PointerEventData eventData) {
        theText.color = color;
    }

    public void OnPointerUp(PointerEventData eventData) {
        theText.color = Color.red;
    }
}

