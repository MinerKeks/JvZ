using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = -5f;
    Vector3 velocity;
    GameObject Player;
    // Use this for initialization

    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(this.gameObject);//Im dead (✖╭╮✖) 
    }
    // Update is called once per frame
    void Update () {
        velocity.x = bulletSpeed * Time.deltaTime;
        transform.Translate(velocity);
    }
    
}
