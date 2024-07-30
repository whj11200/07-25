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

        // �Ʒ������� �巡���ߴ��� �Ǻ�
        if (direction.y < 0)
        {
            Debug.Log("�Ʒ������� �巡���߽��ϴ�.");
            //Drag.draggingItem.GetComponent<ItemInfo>().itemData = null;
        }
        else
        {
            Debug.Log("�ٸ� �������� �巡���߽��ϴ�.");
        }
    }
}

