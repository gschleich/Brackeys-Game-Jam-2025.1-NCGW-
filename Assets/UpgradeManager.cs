using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private GameObject spikePreviewPrefab; // Ghost spike for visual feedback
    [SerializeField] private ScrapText scrapText;
    [SerializeField] private UpgradeMenuToggle menuToggle;
    [SerializeField] private LayerMask spikeLayer;

    private bool isPlacingSpike = false;
    private GameObject currentPreview;

    private void Update()
    {
        if (isPlacingSpike)
        {
            HandleSpikePlacement();
        }
    }

    public void BuySpikes()
    {
        int spikeCost = 5;
        if (scrapText.HasEnoughScrap(spikeCost))
        {
            scrapText.UseScrap(spikeCost);
            menuToggle.CloseMenuWithoutResumingTime();
            isPlacingSpike = true;

            // Create a preview object
            currentPreview = Instantiate(spikePreviewPrefab);
        }
        else
        {
            Debug.Log("Not enough scrap!");
        }
    }

    private void HandleSpikePlacement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 snappedPosition = SnapToGrid(mousePosition);

        // Update preview position
        if (currentPreview != null)
        {
            currentPreview.transform.position = snappedPosition;

            // Change color based on placement validity
            bool occupied = IsOccupied(snappedPosition);
            SetPreviewColor(occupied ? Color.red : Color.green);

            if (Input.GetMouseButtonDown(0) && !occupied)
            {
                PlaceSpike(snappedPosition);
            }
        }
    }

    private void PlaceSpike(Vector2 position)
    {
        Instantiate(spikePrefab, position, Quaternion.identity);
        Destroy(currentPreview);
        isPlacingSpike = false;
        menuToggle.OpenMenuWithoutResumingTime();
    }

    private Vector2 SnapToGrid(Vector2 position)
    {
        float gridSize = 1f; // Adjust based on your grid size
        float snappedX = Mathf.Floor(position.x / gridSize) * gridSize + gridSize / 2f;
        float snappedY = Mathf.Floor(position.y / gridSize) * gridSize + gridSize / 2f;
        return new Vector2(snappedX, snappedY);
    }

    private bool IsOccupied(Vector2 position)
    {
        float checkRadius = 0.4f; // Slightly less than half the grid size
        Collider2D hit = Physics2D.OverlapCircle(position, checkRadius, spikeLayer);
        return hit != null;
    }

    private void SetPreviewColor(Color color)
    {
        SpriteRenderer renderer = currentPreview.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }
}
