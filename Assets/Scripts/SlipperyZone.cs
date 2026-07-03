using UnityEngine;

// Hazard #2: an ice patch that makes steering sluggish (loss of control).
public class SlipperyZone : MonoBehaviour
{
    public float duration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null) pm.ApplySlippery(duration);

            if (AudioManager.Instance != null) AudioManager.Instance.PlayIce();
        }
    }
}