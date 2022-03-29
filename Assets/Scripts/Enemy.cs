using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int randomInt;
    public int paths;
    public int pathsOnMap;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int goldValue = 50;
    
    public GameObject deathEffect;

    [Header("Unity stuff")]
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        randomInt = Random.Range(1, (WayPoints.pathsOnMap + 1));
        
    }

    public void TakeDamage(float damageAmount)
    {   
        health -= damageAmount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {
            HorribleDeath();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    void HorribleDeath()
    {
        isDead = true;
        PlayerStats.Money += goldValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

    
}
