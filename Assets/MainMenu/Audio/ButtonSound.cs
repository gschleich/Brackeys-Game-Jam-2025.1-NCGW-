using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Singleton instance
    private static ButtonSound instance;

    void Awake()
    {
        // Check if there is already an instance of ButtonSound
        if (instance == null)
        {
            // If not, set this as the instance and mark it to not be destroyed
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists and it's not this, destroy this GameObject
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource component found on this GameObject.");
        }
    }

    // Method to play the sound
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
