using UnityEngine;

public class GarageDoorController : MonoBehaviour
{
    public Animator doorAnimator;
    private bool isOpeningOrClosing = false;
    private bool isOpen = false;

    private void Start()
    {
        doorAnimator.Play("Closed"); // Initial state set to Closed
        isOpen = false;
    }

    public void OpenDoor()
    {
        if (!isOpeningOrClosing && !isOpen)
        {
            isOpeningOrClosing = true;
            doorAnimator.Play("Opening");
        }
    }

    public void CloseDoor()
    {
        if (!isOpeningOrClosing && isOpen)
        {
            isOpeningOrClosing = true;
            doorAnimator.Play("Closing");
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
}
