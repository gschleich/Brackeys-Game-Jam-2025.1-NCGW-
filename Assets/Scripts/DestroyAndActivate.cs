using System.Collections;
using UnityEngine;

public class DestroyAndActivate : MonoBehaviour
{
    public GameObject objectToDestroy;
    public GameObject objectToActivate;

    // Call this method to start the process
    public void TriggerAction()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        if (objectToActivate != null)
        {
            StartCoroutine(ActivateAfterDelay(1f)); // Can change game over delay
        }
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectToActivate.SetActive(true);
    }
}