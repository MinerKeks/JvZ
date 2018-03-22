using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = -5f;
    Vector3 velocity;
    Ikillable enemy;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        enemy = collider.gameObject.GetComponent<Ikillable>();
        if (enemy != null)
        {
            enemy.TakeDamage(1,Mathf.Sign(bulletSpeed));
        }
        Destroy(gameObject);
    }
    void Update()
    {
        velocity.x = bulletSpeed * Time.deltaTime;
        transform.Translate(velocity);
    }

}
