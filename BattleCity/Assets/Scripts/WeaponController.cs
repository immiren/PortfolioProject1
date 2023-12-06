using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    GameObject cannonBall;
    Projectile cannon;
    [SerializeField] int speed = 1;
    private float nextShot = 0.15f;
    [SerializeField] private float fireDelay = 0.5f;
    public bool isDoubleBullets = false;
    public void Fire() // changed from if projectile doesnt exist to cooldown. change from setactive to instantiate?
    {
        if (Time.time > nextShot)
        {
            cannonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            cannon = cannonBall.GetComponent<Projectile>();
            cannon.speed = speed;

            if (isDoubleBullets)
            {
                StartCoroutine(InstantiateDelayedProjectile(fireDelay * 0.5f));
            }
            nextShot = Time.time + fireDelay;
        }
    }
    private IEnumerator InstantiateDelayedProjectile(float delay)
    {
        yield return new WaitForSeconds(delay);

        cannonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        cannon = cannonBall.GetComponent<Projectile>();
        cannon.speed = speed;
    }
}
