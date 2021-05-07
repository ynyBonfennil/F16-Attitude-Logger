using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AirspeedIndicatorDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector3 prevPointerPos;
    private const long MAX_ANCHORED_POS_Y = 469;
    private const long MIN_ANCHORED_POS_Y = -469;

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
        float newAnchoredPositionY = rectTransform.anchoredPosition.y + deltaY;
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
