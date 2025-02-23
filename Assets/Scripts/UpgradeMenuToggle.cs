using UnityEngine;

public class UpgradeMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private WaveManager waveManager; // Reference to the WaveManager
    public bool IsMenuOpen => isMenuOpen;
    private bool isMenuOpen = false;

    void Update()
    {
        // Only allow toggling during the Preparation Phase
        if (waveManager.currentState == WaveManager.GameState.Preparation && Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUpgradeMenu();
        }
    }

    public void ToggleUpgradeMenu()
    {
        isMenuOpen = !isMenuOpen;
        upgradeMenu.SetActive(isMenuOpen);
        Time.timeScale = isMenuOpen ? 0f : 1f;
    }

    public void CloseMenuWithoutResumingTime()
    {
        isMenuOpen = false;
        upgradeMenu.SetActive(false);
        // Time stays frozen
    }

    public void OpenMenuWithoutResumingTime()
    {
        isMenuOpen = true;
        upgradeMenu.SetActive(true);
        // Time stays frozen
    }
}
