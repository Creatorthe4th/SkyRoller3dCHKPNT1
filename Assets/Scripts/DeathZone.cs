using UnityEngine;

// OPTIONAL. The GameManager now detects falls via a Y-threshold, which works
// for an endless track (a fixed trigger box can't span infinite Z). You can
// delete your old DeathZone object entirely. If you'd rather keep a trigger,
// this version routes into the same game-over flow instead of hard-reloading.
public class DeathZone : MonoBehaviour
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
