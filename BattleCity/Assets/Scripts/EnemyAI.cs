using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Movement
{
    Rigidbody2D rb2d;
    float h,v;
    enum Direction { Up, Down, Left, Right}; // more readable than numbers yippee
    Direction[] direction = { Direction.Up, Direction.Down, Direction.Left, Direction.Right};
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        RandomDirection();
    }

    void OnCollisionEnter2D(Collision2D collision) // Changes dir when running into a wall
    {
        RandomDirection();
    }

    void FixedUpdate()
    {
        if (h != 0 && !isMoving) StartCoroutine(MoveHorizontal(h,rb2d));
        else if (v != 0 && !isMoving) StartCoroutine(MoveVertical(v,rb2d));
    }
    
    public void RandomDirection()
    {
        CancelInvoke("RandomDirection");
        Direction selection = direction[Random.Range(0,4)];
        if (selection == Direction.Up)
        {
            v = 1;
            h = 0;
        }
        if (selection == Direction.Down)
        {
            v = -1;
            h = 0;
        }
        if (selection == Direction.Left)
        {
            v = 0;
            h = -1;
        }
        if (selection == Direction.Right)
        {
            v = 0;
            h = 1;
        }
        Invoke("RandomDirection", Random.Range(3,6)); // invoke a new direction every 3-5 seconds for irrationality yippee
    }
}
