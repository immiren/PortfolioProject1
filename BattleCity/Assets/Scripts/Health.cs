using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int actualHealth = 1;
    int currentHealth;
    Animator anim;
    Rigidbody2D rb2d;

    void Start()
    {
        if (gameObject.CompareTag("Fast")) actualHealth = 2;
        else if (gameObject.CompareTag("Big")) actualHealth = 3;
        else if (gameObject.CompareTag("Armored")) actualHealth = 4;
        SetHealth();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            rb2d.velocity = Vector2.zero;
            anim.SetTrigger("killed");
        }
    }
    public void SetHealth()
    {
        currentHealth = actualHealth;
    }
    public void SetInvincible()
    {
        currentHealth = 1000;
    }

    void Death(){
        GamePlayManager GPM = GameObject.Find("Canvas").GetComponent<GamePlayManager>();
        if (gameObject.CompareTag("Player")){
            GPM.SpawnPlayer();
        }else{ // add stats for destroyed tanks
            if (gameObject.CompareTag("Small")) MasterTracker.smallTankDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTankDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTankDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTankDestroyed++;
        }
        Destroy(gameObject);
    }
}
