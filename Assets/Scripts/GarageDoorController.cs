using UnityEngine;

public class GarageDoorController : MonoBehaviour
{
    public Animator doorAnimator; // Reference to the door's Animator

    private int enemyCount = 0; // Tracks how many enemies are in the trigger
    private bool isOpeningOrClosing = false; // To track if the door is in the opening or closing state
    private bool isOpen = false; // Tracks if the door is open

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyCount++;

            if (enemyCount == 1 && !isOpeningOrClosing && !isOpen)
            {
                isOpeningOrClosing = true;
                doorAnimator.Play("Opening");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyCount--;

            if (enemyCount <= 0 && !isOpeningOrClosing && isOpen)
            {
                isOpeningOrClosing = true;
                doorAnimator.Play("Closing");
            }
        }
    }

    // Called by an Animation Event at the end of the Opening animation
    public void OnOpeningComplete()
    {
        doorAnimator.Play("Open");
        isOpeningOrClosing = false;
        isOpen = true;
    }

    // Called by an Animation Event at the end of the Closing animation
    public void OnClosingComplete()
    {
        doorAnimator.Play("Closed");
        isOpeningOrClosing = false;
        isOpen = false;
    }

    private void Start()
    {
        doorAnimator.Play("Closed"); // Initial state set to Closed
        isOpen = false;
    }
}
