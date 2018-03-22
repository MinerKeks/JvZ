using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IKillable
{
    public float maxCAMABULLETSpeed = 4f;
    Killable k = new Killable();
    Rigidbody2D rb2d = new Rigidbody2D();
    GameObject Player;
    // Use this for initialization

    public void Die(GameObject gameObject)
    {
        k.Die(gameObject);
    }
    public int TakeDamage(int damage, int health)
    {
        return k.TakeDamage(damage, health);
    }
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }
	void FixedUpdate()
    {
        if (Player.transform.position.x > gameObject.transform.position.x) //Если Камапуля слева
        {
            rb2d.AddForce(Vector2.left * maxCAMABULLETSpeed);
        }
        else//Если Камапуля справа
        {
            rb2d.AddForce(Vector2.right * maxCAMABULLETSpeed);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Die(this.gameObject);//Im dead (✖╭╮✖) 
    }
    // Update is called once per frame
    void Update () {
        Debug.Log(rb2d.velocity.magnitude);
	}
    
}
