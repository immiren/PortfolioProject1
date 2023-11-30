using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Check for horizontal or vertical movement input, and apply possible rotation.
//Works for player and enemy tanks

public abstract class Movement : MonoBehaviour
{
    public int speed = 5;
    protected bool isMoving = false;
    //EnemyAI enemy;

    protected IEnumerator MoveHorizontal(float movementHorizontal, Rigidbody2D rb2d)
    {
        isMoving = true;

        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        Quaternion rotation = Quaternion.Euler(0,0,-movementHorizontal*90f);
        transform.rotation = rotation;

        float movementProgress = 0f; // checks progress towards destination. stopped at 1 to avoid tank being in a weird spot
        Vector2 movement, endPos;

        while (movementProgress < Mathf.Abs(movementHorizontal))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f); // gives value between 0 and 1, 0 = start and 1 = end point

            movement = new Vector2(speed*Time.deltaTime * movementHorizontal, 0f); // calculated required movement on x-axis
            endPos = rb2d.position+movement; // gives end position -> start plus req movement

            if (movementProgress == 1) endPos = new Vector2(Mathf.Round(endPos.x), endPos.y); // ensures clean end point yea
            rb2d.MovePosition(endPos);
            //if (enemy != null) { enemy.ResetTriedDirection(); }
            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
    }
    protected IEnumerator MoveVertical(float movementVertical, Rigidbody2D rb2d)
    {
        isMoving = true;

        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        Quaternion rotation;

        if (movementVertical < 0) 
        {
            rotation = Quaternion.Euler(0,0, movementVertical * 180f);
        }
        else
        {
            rotation = Quaternion.Euler(0,0,0);
        }
        transform.rotation = rotation;

        float movementProgress = 0f; // checks progress towards destination. stopped at 1 to avoid tank being in a weird spot
        Vector2 movement, endPos;

        while (movementProgress < Mathf.Abs(movementVertical))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f);

            movement = new Vector2(0f, speed * Time.deltaTime * movementVertical);
            endPos = rb2d.position+movement;

            if (movementProgress == 1) endPos = new Vector2(endPos.x, Mathf.Round(endPos.y));
            rb2d.MovePosition(endPos);
            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
    }
}
