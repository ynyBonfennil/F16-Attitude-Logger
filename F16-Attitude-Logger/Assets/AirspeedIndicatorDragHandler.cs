using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AirspeedIndicatorDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public float sensitivity = 0.5f;
    private RectTransform rectTransform;
    private Vector3 prevPointerPos;
    private const float MAX_ANCHORED_POS_Y = 481.8f;
    private const float MIN_ANCHORED_POS_Y = -280.0f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        prevPointerPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaY = eventData.position.y - prevPointerPos.y;
        float newAnchoredPositionY = rectTransform.anchoredPosition.y + deltaY * sensitivity;
        if (newAnchoredPositionY < MIN_ANCHORED_POS_Y)
        {
            newAnchoredPositionY = MIN_ANCHORED_POS_Y;
        }
        else if (newAnchoredPositionY > MAX_ANCHORED_POS_Y)
        {
            newAnchoredPositionY = MAX_ANCHORED_POS_Y;
        }
        rectTransform.anchoredPosition = new Vector3(0, newAnchoredPositionY, 0);

        prevPointerPos = eventData.position;
    }
}
