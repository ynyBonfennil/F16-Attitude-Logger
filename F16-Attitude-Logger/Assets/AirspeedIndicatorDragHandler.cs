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
            -127.3 --> 0
            -1159.1 --> 65
            airspeed = - anchoredPosition.y * 0.0629967 - 8.01947991
        */
        debugOut.text = (- rectTransform.anchoredPosition.y * 0.0629967f - 8.01947991f).ToString();
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
