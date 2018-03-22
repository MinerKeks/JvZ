using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ObjectStuff))]
public class Player : MonoBehaviour, Ikillable {
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float attackCD = .25f;
    private float speed = 8f;
    private float jumpVelocity = 13f;
    private float cdLeft = 0;
    private float stunTimer = 0;
    private float gravity = -20;
    private float deathCD = 3.5f;
    private bool dead = false;
    private Animator animator;
    public GameObject bullerPrefab;
    private GameObject gameController;
    private ObjectStuff controller;
    private Vector3 velocity;
    void Start () {
        controller = GetComponent<ObjectStuff>();
        animator = GetComponent<Animator>();
        gameController =  GameObject.Find("GameController");
        gameController.GetComponent<GameController>().setStartValue(health);
    }
    void Update()
    {
        if (dead)
            deathCD -=Time.deltaTime;
        if (dead && deathCD <= 0)
        {
            DontDestroyOnLoad(gameController); SceneManager.LoadScene("GameOver");
        }
        if (cdLeft > 0)
            cdLeft -= Time.deltaTime;
        if (stunTimer > 0)
            stunTimer -= Time.deltaTime;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButton("Fire") && !dead)
            Shoot(Mathf.Sign(transform.localScale.x));
        else
            animator.SetBool("Shooting", false);
        if (Input.GetButtonDown("Jump") && controller.collisions.bellow && !dead)
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            velocity.y = jumpVelocity;
        }
        if (!Input.GetButton("Jump") || velocity.y < 0.1)
            velocity.y += gravity * 2f * Time.deltaTime;
        if (stunTimer <= 0 && !dead)
            velocity.x = input.x * speed;
        else if (stunTimer <= 0 && dead)
            velocity.x = 0;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (controller.collisions.above || controller.collisions.bellow)
            velocity.y = 0;
        animator.SetBool("Airborne", !controller.collisions.bellow);
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        if (input.x != 0 && !dead)
            transform.localScale = new Vector3(Mathf.Sign(input.x) * 15, 15, 15);

    }
    void Shoot(float direction)
    {
        if (cdLeft <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Shoot");
            animator.SetBool("Shooting", true);
            cdLeft = attackCD;
            Vector3 tip = transform.position;
            tip.y += .8f;
            tip.x += .8f * direction;
            GameObject bullet = Instantiate(bullerPrefab, tip, transform.rotation);
            Bullet b = bullet.GetComponent<Bullet>();
            b.bulletSpeed = b.bulletSpeed * direction;
        }
    }
    public void TakeDamage(int damage, float direction)
    {
        if (!dead)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            stunTimer = 0.5f;
            velocity.y = 10 * damage;
            velocity.x = direction * 8 * damage;
            health -= damage;
            gameController.GetComponent<GameController>().ChangeHealthBar(health);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Defeat");
        animator.SetTrigger("Dead");
        dead = true;
    }
}
