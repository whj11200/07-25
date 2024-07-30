using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDisance : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 endPosition = Input.mousePosition;
        Vector2 direction = endPosition - startPosition;

        // 아래쪽으로 드래그했는지 판별
        if (direction.y < 0)
        {
            Debug.Log("아래쪽으로 드래그했습니다.");
            //Drag.draggingItem.GetComponent<ItemInfo>().itemData = null;
        }
        else
        {
            Debug.Log("다른 방향으로 드래그했습니다.");
        }
    }
}

