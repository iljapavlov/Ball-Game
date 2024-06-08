using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBlock : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    private bool isDraggable = true;
    private bool isInInventory = true;

    public void SetDraggable(bool draggable)
    {
        isDraggable = draggable;
    }
    public void OnMouseDown()
    {
        if (!isDraggable) return;
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    public void OnMouseDrag()
    {
        if (!isDraggable) return;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        isInInventory = false; // if user moved block -> it is considered to be removed from the inventory
    }
    public bool IsInInventory()
    {
        return isInInventory;
    }
}
