using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPowerUp : PowerUp
{
    public float dashSpeedIncrease = 5f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.dashSpeed += dashSpeedIncrease;
                powerupSignal.Raise();
                Destroy(gameObject);
            }
        }
    }
}


