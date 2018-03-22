using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectStuff))]
public class Zombie : MonoBehaviour, Ikillable
{

    // Use this for initialization
    [SerializeField]
    private float health = 3;
    [SerializeField]
    float biteCD = .5f;
    [SerializeField]
    float attakRange = 1;
    float cdLeft = 0;
    float knokbacktimer = 0;
    float despawn = 2;
    private float gravity = -20;
    private float jumpVelocity = 10f;
    private float speed = 4f;
    bool dead = false;
    GameObject target;
    Animator animator;
    GameObject gameController;
    ObjectStuff controller;
    Vector3 velocity;
    Ikillable player;
    void Start()
    {
        controller = GetComponent<ObjectStuff>();
        animator = gameObject.GetComponent<Animator>();
        gameController = GameObject.Find("GameController");
    }
    void Update()
    {
        if (dead)
            despawn -= Time.deltaTime;
        if (dead && despawn <= 0)
            Destroy(gameObject);
        if (knokbacktimer > 0)
            knokbacktimer -= Time.deltaTime;
        target = FindClosestTarget();
        animator.SetBool("Airborne", !controller.collisions.bellow);
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        velocity.y += gravity * Time.deltaTime;
        if (!(Vector3.Distance(target.transform.position, transform.position) <= attakRange) && !dead)
        {
            float direction = Mathf.Sign(target.transform.position.x - transform.position.x);
            if(knokbacktimer <= 0)
                velocity.x = direction * speed;
            if ((controller.collisions.left || controller.collisions.right) && direction == System.Math.Sign(velocity.x))
            {
                velocity.y = jumpVelocity;
                FindObjectOfType<AudioManager>().Play("ZombieJump");
            }
            if (direction != 0)
                transform.localScale = new Vector3(direction * 15, 15, 15);
        }
        else if(controller.collisions.bellow && !dead)
        {
            Bite(target.GetComponent<Ikillable>());
            velocity.x = 0;
        }
        else if(knokbacktimer <= 0)
            velocity.x = 0;
        controller.Move(velocity * Time.deltaTime);
        if (controller.collisions.above || controller.collisions.bellow)
            velocity.y = 0;
        if (cdLeft > 0)
            cdLeft -= Time.deltaTime;
    }
    void Bite(Ikillable target)
    {
        if(cdLeft <= 0)
        {
            cdLeft = biteCD;
            target.TakeDamage(1, Mathf.Sign(gameObject.transform.localScale.x));
        }

    }
    GameObject FindClosestTarget()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Target");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public void TakeDamage(int damage,float direction)
    {
        FindObjectOfType<AudioManager>().Play("ZombieHit");
        knokbacktimer = 0.5f;
        velocity.y = 5 * damage;
        velocity.x = direction * 5 * damage;
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameController.GetComponent<GameController>().ChangeScore(25);
        dead = true;
        animator.SetTrigger("Dead");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

}
