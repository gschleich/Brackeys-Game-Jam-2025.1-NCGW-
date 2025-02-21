using UnityEngine;

public class AnimatorToggle : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject.");
        }
    }

    // Method to toggle the Animator's enabled state
    public void ToggleAnimator()
    {
        if (animator != null)
        {
            animator.enabled = !animator.enabled;
        }
    }
}
