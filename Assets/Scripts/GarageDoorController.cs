using UnityEngine;

public class GarageDoorController : MonoBehaviour
{
    public Animator doorAnimator; // Reference to the door's Animator
    public string closedAnimationBool = "IsClosed"; // Bool parameter for closed state
    public string openingAnimationBool = "IsOpening"; // Bool parameter for opening state
    public string openAnimationBool = "IsOpen"; // Bool parameter for open state
    public string closingAnimationBool = "IsClosing"; // Bool parameter for closing state

    private int enemyCount = 0; // Keeps track of how many enemies are in the trigger
    private bool isOpeningOrClosing = false; // To track if the door is in the opening or closing state

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Increase enemy count when an enemy enters the trigger
            enemyCount++;

            // If it's the first enemy, start the opening animation
            if (enemyCount == 1 && !isOpeningOrClosing)
            {
                isOpeningOrClosing = true;
                doorAnimator.SetBool(openingAnimationBool, true); // Start opening animation
                doorAnimator.SetBool(closingAnimationBool, false); // Ensure closing animation is not active
                doorAnimator.SetBool(closedAnimationBool, false); // Ensure closed state is not active
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Decrease enemy count when an enemy exits the trigger
            enemyCount--;

            // If no enemies are left, start closing animation
            if (enemyCount <= 0 && !isOpeningOrClosing)
            {
                isOpeningOrClosing = true;
                doorAnimator.SetBool(closingAnimationBool, true); // Start closing animation
                doorAnimator.SetBool(openingAnimationBool, false); // Ensure opening animation is not active
                doorAnimator.SetBool(openAnimationBool, false); // Ensure open state is not active
            }
        }
    }

    // This method will be called after the Opening animation finishes
    public void OnOpeningComplete()
    {
        doorAnimator.SetBool(openAnimationBool, true); // Set door as fully open
        doorAnimator.SetBool(openingAnimationBool, false); // Stop opening animation
        doorAnimator.SetBool(closedAnimationBool, false); // Ensure closed state is not active
        isOpeningOrClosing = false; // Reset flag after opening is complete
    }

    // This method will be called after the Closing animation finishes
    public void OnClosingComplete()
    {
        doorAnimator.SetBool(closedAnimationBool, true); // Set door as fully closed
        doorAnimator.SetBool(closingAnimationBool, false); // Stop closing animation
        doorAnimator.SetBool(openAnimationBool, false); // Ensure open state is not active
        isOpeningOrClosing = false; // Reset flag after closing is complete

        // Ensure IsClosing is set to false when the door is closed
        doorAnimator.SetBool(closingAnimationBool, false); // Reset closing state
        doorAnimator.SetBool(closedAnimationBool, true); // Ensure closed state is true
    }

    private void Start()
    {
        doorAnimator.SetBool(closedAnimationBool, true); // Initial state set to Closed
        doorAnimator.SetBool(openAnimationBool, false); // Ensure the door is not in the Open state initially
    }
}
