using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompassDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Text debugOut;
    private RectTransform rectTransform;
    private Vector3 prevPointerPos;
    private const float MAX_ANCHORED_POS_X = 100;
    private const float MIN_ANCHORED_POS_X = -3400.0f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        debugOut.text = (- rectTransform.anchoredPosition.x / 94.4f).ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        prevPointerPos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaX = eventData.position.x - prevPointerPos.x;
        float newAnchoredPositionX = rectTransform.anchoredPosition.x + deltaX;
        if (newAnchoredPositionX < MIN_ANCHORED_POS_X)
        {
            newAnchoredPositionX = MIN_ANCHORED_POS_X;
        }
        else if (newAnchoredPositionX > MAX_ANCHORED_POS_X)
        {
            newAnchoredPositionX = MAX_ANCHORED_POS_X;
        }
        rectTransform.anchoredPosition = new Vector3(newAnchoredPositionX, 0, 0);

        prevPointerPos = eventData.position;
    }
}
