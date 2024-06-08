using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI; // Assign the inventory UI element in the Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        if (!inventoryUI.activeSelf)
        {
            // Ensure blocks that are dragged out of the inventory remain visible
            DraggableBlock[] draggableBlocks = FindObjectsOfType<DraggableBlock>();
            foreach (var block in draggableBlocks)
            {
                if (!block.IsInInventory())
                {
                    Debug.Log("set");
                    block.gameObject.SetActive(true);
                }
            }
        }
    }
}
