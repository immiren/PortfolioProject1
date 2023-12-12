using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] GamePlayManager GPM;

    private void Start()
    {
        GPM = GameObject.FindAnyObjectByType<Canvas>().GetComponent<GamePlayManager>();
    }
    public void SpawnPowerUp()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Spawning");
    }
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
                GPM.isShield = true;
            }
            if (this.CompareTag("SpeedPU"))
            {
                Movement playerMovement = collision.GetComponent<Movement>();
                playerMovement.SetSpeed();
                GPM.isSpeed = true;
            }
            if (this.CompareTag("BulletPU"))
            {
                wc.isDoubleBullets = true;
                GPM.isBullets = true;
            }
            GPM.UpdatePowerUps();
        }
        else
        {
            Debug.LogWarning("WeaponController not found in children of the player.");
        }
    }
}
