using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private float speed;

    InputAction aimAction;
    InputAction shootAction;

    private GameObject bulletSpawner;
    private GameObject healthManager;

    private bool onBoss = false;

    // AWAKE:
    // - get the body
    // - get bulletSpawner
    // - set up actions
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        bulletSpawner = GameObject.Find("BulletSpawner");
        healthManager = GameObject.Find("HealthBars");

        aimAction = InputSystem.actions.FindAction("Aim");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    // UPDATE:
    // - move the crosshair
    // - handle shooting
    void Update()
    {
        // get moveValue and use it to move
        Vector2 aimValue = aimAction.ReadValue<Vector2>();

        if ((Mathf.Abs(aimValue.x) > 0 || Mathf.Abs(aimValue.y) > 0))
        {
            body.linearVelocityX = aimValue.x * speed;
            body.linearVelocityY = aimValue.y * speed;
        }
        else
            body.linearVelocity = Vector2.zero;

        // handle shooting
        if (shootAction.IsPressed())
        {
            // check if it's on the boss
            if(onBoss == true)
            {
                print("Boss hit!");

                // damage the boss
                healthManager.GetComponent<HealthManager>().RemoveBossHealth();
            }

            // reset bullet spawner
            bulletSpawner.GetComponent<BulletSpawner>().ResetCanBullet();

            // disappear
            Destroy(body.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
            onBoss = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
            onBoss = false;
    }
}
