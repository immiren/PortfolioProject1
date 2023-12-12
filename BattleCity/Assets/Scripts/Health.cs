using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int actualHealth = 1;
    public int currentHealth;
    Animator anim;
    Rigidbody2D rb2d;
    public bool isShieldActive = false;
    [SerializeField] GamePlayManager GPM;

    void Start()
    {
        if (gameObject.CompareTag("Fast")) actualHealth = 2;
        else if (gameObject.CompareTag("Big")) actualHealth = 3;
        else if (gameObject.CompareTag("Armored")) actualHealth = 4;
        SetHealth();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        GPM = GameObject.FindAnyObjectByType<Canvas>().GetComponent<GamePlayManager>();
    }

    public void TakeDamage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            GPM.isShield = false;
            GPM.UpdatePowerUps();
            return;
        }
        currentHealth--;
        if (currentHealth <= 0)
        {
            rb2d.velocity = Vector2.zero;
            anim.SetTrigger("killed");
            if (gameObject.CompareTag("Player")) GPM.RemovePowerUps();
        }
    }
    public void SetHealth()
    {
        if (gameObject.CompareTag("Fast")) actualHealth = 2;
        else if (gameObject.CompareTag("Big")) actualHealth = 3;
        else if (gameObject.CompareTag("Armored")) actualHealth = 4;
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
            if (gameObject.CompareTag("Small")) MasterTracker.smallTanksDestroyed++;
            else if (gameObject.CompareTag("Fast")) MasterTracker.fastTanksDestroyed++;
            else if (gameObject.CompareTag("Big")) MasterTracker.bigTanksDestroyed++;
            else if (gameObject.CompareTag("Armored")) MasterTracker.armoredTanksDestroyed++;
        }
        Destroy(gameObject);
    }
    public void SetShield()
    {
        isShieldActive = true;
    }
}
