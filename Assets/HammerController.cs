using UnityEngine;

public class HammerController : MonoBehaviour
{
    public Transform player;
    public Transform hammerPivot; // The pivot that rotates
    public SpriteRenderer hammerSprite;
    public Animator animator;
    public CircleCollider2D hammerCollider; // Reference to the collider

    private bool isSwinging = false;
    private bool isFlipped = false; // Track current flip state

    void Update()
    {
        HandleRotation();
        HandleSwing();
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        mousePosition.y += -1; // Offset the Y position by -1

        Vector3 direction = mousePosition - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        hammerPivot.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        bool shouldFlip = mousePosition.x < player.position.x;
        if (shouldFlip != isFlipped) // Only update if flip state changes
        {
            isFlipped = shouldFlip;
            hammerSprite.flipY = isFlipped;

            // Flip the collider by modifying its offset
            Vector2 colliderOffset = hammerCollider.offset;
            colliderOffset.y *= -1; // Mirror the y offset
            hammerCollider.offset = colliderOffset;
        }
    }

    void HandleSwing()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            isSwinging = true;
            animator.SetBool("isSwinging", true);

            // Use animation length to reset swinging
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ResetSwing", animationLength);
        }
    }

    void ResetSwing()
    {
        isSwinging = false;
        animator.SetBool("isSwinging", false);
    }
}
