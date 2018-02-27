using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonGrowShrink : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {

    private Vector3 originalSize;

	public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalSize * 1.2f;
    }
    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = originalSize * 1.2f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalSize;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = originalSize;
    }

    void Awake()
    {
        originalSize = new Vector3(1f, 1f, 1f);
    }

	// Update is called once per frame
	void Update () {
		
	}

 
}
