using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AirspeedIndicatorDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Text debugOut;
    private RectTransform rectTransform;
    private Vector3 prevPointerPos;
    private const float MAX_ANCHORED_POS_Y = 0.0f;
    private const float MIN_ANCHORED_POS_Y = -1018f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        /*
            -102.49 --> 0
            -165 --> 5
            -921 --> 65
            airspeed = - anchoredPosition.y * 12.5924 - 102.49
        */
        debugOut.text = (- rectTransform.anchoredPosition.y * 0.07941259f - 8.13899635f).ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        prevPointerPos = Input.mousePosition;
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
