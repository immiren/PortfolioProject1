using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    Rigidbody2D rb2d;
    float h, v;
    WeaponController wc;
    void Start()
    {
        wc = GetComponentInChildren<WeaponController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wc.Fire();
        }
    }
    void FixedUpdate()
    {
        if (h != 0 && !isMoving) StartCoroutine(MoveHorizontal(h,rb2d));
        else if (v != 0 && !isMoving) StartCoroutine(MoveVertical(v,rb2d));
        if (!isMoving) { rb2d.velocity = Vector3.zero; }
    }
}
