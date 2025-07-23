using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnHoverColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI theText;
    public Color onHover;
    public Color offHover;


    private void Awake()
    {
        theText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = onHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = offHover;
    }
}
