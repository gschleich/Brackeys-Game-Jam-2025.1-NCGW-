using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] private List<Image> slotImages; // UI slot images
    [SerializeField] private Sprite selectedSprite;  // Active slot sprite
    [SerializeField] private Sprite deselectedSprite; // Inactive slot sprite
    [SerializeField] private GameObject[] slotObjects; // GameObjects to activate/deactivate

    private int selectedIndex = 0;

    private void Start()
    {
        if (slotImages.Count != slotObjects.Length)
        {
            Debug.LogError("Slot UI and GameObjects count mismatch!");
            return;
        }

        SelectSlot(0); // Default to Slot 1
    }

    private void Update()
    {
        HandleNumberInput();
        HandleScrollInput();
    }

    private void HandleNumberInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
    }

    private void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f) // Scroll Up
        {
            SelectSlot((selectedIndex + 1) % slotImages.Count);
        }
        else if (scroll < 0f) // Scroll Down
        {
            SelectSlot((selectedIndex - 1 + slotImages.Count) % slotImages.Count);
        }
    }

    private void SelectSlot(int index)
    {
        if (index < 0 || index >= slotImages.Count) return; // Prevent out-of-range errors

        // Deselect previous slot
        if (selectedIndex >= 0 && selectedIndex < slotImages.Count)
        {
            slotImages[selectedIndex].sprite = deselectedSprite;
            slotObjects[selectedIndex].SetActive(false);
        }

        // Select new slot
        selectedIndex = index;
        slotImages[selectedIndex].sprite = selectedSprite;
        slotObjects[selectedIndex].SetActive(true);
    }
}
