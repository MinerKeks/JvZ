using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour, Ikillable {
    float Health = 30;
    float MaxHealth;
    float deadCD = 3.5f;
    bool dead = false;
    private RectTransform BaseHPBar;
    void Start()
    {
        BaseHPBar = (GameObject.Find("BaseHPBar").transform as RectTransform);
        MaxHealth = Health;
    }
    void Update()
    {
        if (dead)
            deadCD -= Time.deltaTime;
        if (dead && deadCD <= 0)
        {
            DontDestroyOnLoad(GameObject.Find("GameController")); SceneManager.LoadScene("GameOver");
        }
    }
    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Defeat");
        dead = true;
    }
    public void TakeDamage(int damage, float direction)
    {
        if (!dead)
        {
            FindObjectOfType<AudioManager>().Play("BaseHit");
            Health -= damage;
            BaseHPBar.localScale = new Vector3(Health / MaxHealth, BaseHPBar.localScale.y);
            if (Health <= 0)
            {
                Die();

            }
        }
    }
}
