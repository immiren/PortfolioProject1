using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    GameObject cannonBall;
    Projectile cannon;
    [SerializeField] int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        cannonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        cannon = cannonBall.GetComponent<Projectile>();
        cannon.speed = speed;
    }
    public void Fire()
    {
        if (cannonBall.activeSelf == false)
        {
            cannonBall.transform.position = transform.position;
            cannonBall.transform.rotation = transform.rotation;
            cannonBall.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        if (cannonBall != null) cannon.DestroyProjectile();
    }
}
