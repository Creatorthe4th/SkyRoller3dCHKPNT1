using UnityEngine;

// Hazard #1: a trigger zone (mud/tar patch) that slows the player down.
public class SlowZone : MonoBehaviour
{
    [Range(0.1f, 0.9f)] public float slowMultiplier = 0.4f;
    public float duration = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null) pm.ApplySlow(slowMultiplier, duration);
        }
    }
}
