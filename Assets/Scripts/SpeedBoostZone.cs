using UnityEngine;

public class SpeedBoostZone : MonoBehaviour
{
    float boostSpeed = 15f;
    float boostDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedBoost(boostSpeed, boostDuration);
                if (AudioManager.Instance != null) AudioManager.Instance.PlayBoost();
            }
        }
    }
}