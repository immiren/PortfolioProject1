using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    GameObject brickGameObject, steelGameObject;
    Tilemap tilemap;

    [SerializeField] bool toBeDestroyed = false;
    public bool destroySteel = false;

    public int speed = 1;
    Rigidbody2D rb2d;
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
        brickGameObject = GameObject.FindGameObjectWithTag("Brick");
        steelGameObject = GameObject.FindGameObjectWithTag("Steel");
    }

    private void OnEnable()
    {
        if (rb2d != null)
        {
            rb2d.velocity = transform.up * speed;
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector2.zero; // stop cannonball on impact
        tilemap = collision.gameObject.GetComponent<Tilemap>(); 
        if ((collision.gameObject == brickGameObject) || (destroySteel && collision.gameObject == steelGameObject))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
        this.gameObject.SetActive(false);
    }

    void onDisable()
    {
        if (toBeDestroyed)
        {
            Destroy(this.gameObject);
        }
    }
    
    public void DestroyProjectile()
    {
        if (gameObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
        toBeDestroyed = true;
    }
}
