using UnityEngine;
using System.Collections.Generic;

public class HammerController : MonoBehaviour
{
    public Transform player;
    public Transform hammerPivot; // The pivot that rotates
    public SpriteRenderer hammerSprite;
    public Animator animator;
    public CircleCollider2D hammerCollider; // Reference to the collider

    public List<Barricade> barricades = new List<Barricade>(); // Multiple barricades
    public ScrapText scrapText; // Reference to the scrap UI

    private bool isSwinging = false; // Tracks if the hammer is in a swinging state
    private bool canRepair = true; // Ensures only one repair per click
    private bool isFlipped = false; // Track current flip state
    private HashSet<Barricade> collidingBarricades = new HashSet<Barricade>(); // Tracks currently colliding barricades

    void Update()
    {
        HandleRotation();
        HandleSwing();

        // Repair Logic: Only repair if hammer is swinging, player has enough scrap, and canRepair is true
        if (isSwinging && canRepair && scrapText.HasEnoughScrap(1) && collidingBarricades.Count > 0)
        {
            RepairBarricades();
        }
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
        if (shouldFlip != isFlipped)
        {
            isFlipped = shouldFlip;
            hammerSprite.flipY = isFlipped;

            // Flip the collider by modifying its offset
            Vector2 colliderOffset = hammerCollider.offset;
            colliderOffset.y *= -1;
            hammerCollider.offset = colliderOffset;
        }
    }

    void HandleSwing()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            isSwinging = true;
            canRepair = true;
            animator.SetBool("isSwinging", true);

            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ResetSwing", animationLength);
        }
    }

    void ResetSwing()
    {
        isSwinging = false;
        animator.SetBool("isSwinging", false);
        canRepair = false;
    }

    void RepairBarricades()
    {
        foreach (var barricade in collidingBarricades)
        {
            if (scrapText.HasEnoughScrap(1) && barricade.Repair(1))
            {
                scrapText.UseScrap(1);
            }
        }
        canRepair = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barricade"))
        {
            Barricade barricade = other.GetComponent<Barricade>();
            if (barricade != null)
            {
                collidingBarricades.Add(barricade);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Barricade"))
        {
            Barricade barricade = other.GetComponent<Barricade>();
            if (barricade != null)
            {
                collidingBarricades.Remove(barricade);
            }
        }
    }
}
