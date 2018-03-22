using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour, Ikillable {
    // Use this for initialization
    [SerializeField]
    private float health = 6;
    [SerializeField]
    protected float SpawnCoolDown = 15;
    protected float SpawnCoolDownLeft = 0;
    public GameObject enemyPrefab;
    GameObject gameController;
    void Start ()
    {
        gameController = GameObject.Find("GameController");
    }
	void Update ()
    {
        SpawnCoolDownLeft -= Time.deltaTime;
        SpawnCoolDownLeft -= Time.deltaTime;
        if (SpawnCoolDownLeft < 0)
        {
            SpawnCoolDownLeft = SpawnCoolDown;
            Instantiate(enemyPrefab, transform.position, transform.rotation);

        }      
	}

    public void TakeDamage(int damage, float direction)
    {
        FindObjectOfType<AudioManager>().Play("GraveHit");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameController.GetComponent<GameController>().ChangeScore(100);
        Destroy(gameObject);
    }
}
