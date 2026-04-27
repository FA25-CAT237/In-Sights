using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Rigidbody2D body;

    private GameObject healthManager;

    // AWAKE:
    // - get the body
    // - set up the moveAction
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        healthManager = GameObject.Find("HealthBars");
    }

    // UPDATE:
    // - delete and reset bullet spawner if too low
    void Update()
    {
        if(body.transform.position.y < -15)
        {
            // die
            Destroy(body.gameObject);
        }
    }

    // HIT:
    // - damage player
    // - do nothing now actually this really shouldve been in the player script at the start
    public void Hit()
    {
        // hurt player
        // healthManager.GetComponent<HealthManager>().RemovePlayerHealth();
    }
}
