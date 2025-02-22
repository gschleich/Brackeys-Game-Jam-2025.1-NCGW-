using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Spike Prefabs")]
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private GameObject spikePreviewPrefab;

    [Header("Barricade Prefabs")]
    [SerializeField] private GameObject barricadePrefabRight;
    [SerializeField] private GameObject barricadePrefabDown;
    [SerializeField] private GameObject barricadePrefabLeft;
    [SerializeField] private GameObject barricadePrefabUp;
    [SerializeField] private GameObject barricadePreviewRight;
    [SerializeField] private GameObject barricadePreviewDown;
    [SerializeField] private GameObject barricadePreviewLeft;
    [SerializeField] private GameObject barricadePreviewUp;

    [Header("General")]
    [SerializeField] private ScrapText scrapText;
    [SerializeField] private UpgradeMenuToggle menuToggle;
    [SerializeField] private LayerMask spikeLayer;
    [SerializeField] private LayerMask barricadeLayer;

    private bool isPlacingSpike = false;
    private bool isPlacingBarricade = false;
    private GameObject currentPreview;
    private GameObject currentBarricadePreview;
    private int barricadeRotationIndex = 0; // 0 = Right, 1 = Down, 2 = Left, 3 = Up

    private void Update()
    {
        if (isPlacingSpike)
        {
            HandleSpikePlacement();
        }
        if (isPlacingBarricade)
        {
            HandleBarricadePlacement();
        }

        // Rotate barricade on pressing R
        if (isPlacingBarricade && Input.GetKeyDown(KeyCode.R))
        {
            RotateBarricade();
        }
    }

    #region Spikes

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
            bool occupied = IsOccupiedForSpikes(snappedPosition);
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

    private bool IsOccupiedForSpikes(Vector2 position)
    {
        float checkRadius = 0.4f; // Adjust if needed
        Collider2D hit = Physics2D.OverlapCircle(position, checkRadius, spikeLayer);

        return hit != null;
    }

    #endregion

    #region Barricades

    public void BuyBarricade()
    {
        int barricadeCost = 10;
        if (scrapText.HasEnoughScrap(barricadeCost))
        {
            scrapText.UseScrap(barricadeCost);
            menuToggle.CloseMenuWithoutResumingTime();
            isPlacingBarricade = true;

            // Reset the barricade rotation index to the default state (BarricadeRight)
            barricadeRotationIndex = 0; // Default to BarricadeRight
            currentBarricadePreview = Instantiate(barricadePreviewRight); // Start with Right
        }
        else
        {
            Debug.Log("Not enough scrap!");
        }
    }

    private void HandleBarricadePlacement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 snappedPosition = SnapToGrid(mousePosition);

        // Update preview position
        if (currentBarricadePreview != null)
        {
            currentBarricadePreview.transform.position = snappedPosition;

            // Change color based on placement validity
            bool occupied = IsOccupiedForBarricades(snappedPosition);
            SetPreviewColor(occupied ? Color.red : Color.green);

            if (Input.GetMouseButtonDown(0) && !occupied)
            {
                PlaceBarricade(snappedPosition);
            }
        }
    }

    private void PlaceBarricade(Vector2 position)
    {
        GameObject barricadeToPlace = GetCurrentBarricadePrefab();
        Instantiate(barricadeToPlace, position, Quaternion.identity);
        Destroy(currentBarricadePreview);
        isPlacingBarricade = false;
        menuToggle.OpenMenuWithoutResumingTime();
    }

    private GameObject GetCurrentBarricadePrefab()
    {
        switch (barricadeRotationIndex)
        {
            case 0: return barricadePrefabRight; // Right
            case 1: return barricadePrefabDown;  // Down
            case 2: return barricadePrefabLeft;  // Left
            case 3: return barricadePrefabUp;    // Up
            default: return barricadePrefabRight;
        }
    }

    private void RotateBarricade()
    {
        barricadeRotationIndex = (barricadeRotationIndex + 1) % 4;

        // Destroy current preview and show new preview
        Destroy(currentBarricadePreview);
        switch (barricadeRotationIndex)
        {
            case 0:
                currentBarricadePreview = Instantiate(barricadePreviewRight);
                break;
            case 1:
                currentBarricadePreview = Instantiate(barricadePreviewDown);
                break;
            case 2:
                currentBarricadePreview = Instantiate(barricadePreviewLeft);
                break;
            case 3:
                currentBarricadePreview = Instantiate(barricadePreviewUp);
                break;
        }
    }

    private bool IsOccupiedForBarricades(Vector2 position)
    {
        float checkRadius = 0.4f; // Adjust if needed
        Collider2D hit = Physics2D.OverlapCircle(position, checkRadius, barricadeLayer);

        return hit != null;
    }

    #endregion

    private Vector2 SnapToGrid(Vector2 position)
    {
        float gridSize = 1f; // Adjust based on your grid size
        float snappedX = Mathf.Floor(position.x / gridSize) * gridSize + gridSize / 2f;
        float snappedY = Mathf.Floor(position.y / gridSize) * gridSize + gridSize / 2f;
        return new Vector2(snappedX, snappedY);
    }

    private void SetPreviewColor(Color color)
    {
        // If it's barricade preview, set color of barricade preview
        if (currentBarricadePreview != null)
        {
            SpriteRenderer renderer = currentBarricadePreview.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = color;
            }
        }

        // If it's spike preview, set color of spike preview
        if (currentPreview != null)
        {
            SpriteRenderer renderer = currentPreview.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = color;
            }
        }
    }
}
