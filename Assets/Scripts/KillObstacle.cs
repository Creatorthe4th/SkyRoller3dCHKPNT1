using UnityEngine;

// Hazard #3: touching this obstacle (saw / spike / crusher) ends the run.
// Give it a Collider with "Is Trigger" checked.
public class KillObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
        }
    }
}
