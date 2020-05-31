using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
#pragma warning disable 0649
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image bgImage;
#pragma warning restore 0649
    private Color newColor;
    private Color oldColor;

    private void Awake()
    {
        newColor = bgImage.color;
        oldColor = newColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        newColor.a = 1f;
        bgImage.color = newColor;
    }
    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        bgImage.color = oldColor;
    }
}
