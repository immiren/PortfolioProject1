using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivatePowerUp(collision);
            this.gameObject.SetActive(false);
        }
    }
    void ActivatePowerUp(Collider2D collision)
    {
        WeaponController wc = collision.GetComponentInChildren<WeaponController>();

        if (wc != null)
        {
            if (this.CompareTag("ShieldPU"))
            {
                Health playerHealth = collision.GetComponent<Health>();
                playerHealth.SetShield();
            }
            if (this.CompareTag("SpeedPU"))
            {
                Movement playerMovement = collision.GetComponent<Movement>();
                playerMovement.SetSpeed();
            }
            if (this.CompareTag("BulletPU"))
            {
                wc.isDoubleBullets = true;
            }
        }
        else
        {
            Debug.LogWarning("WeaponController not found in children of the player.");
        }
    }
}
