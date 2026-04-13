using UnityEngine;
using UnityEngine.Rendering;

public class BulletSpawner : MonoBehaviour
{
    private Rigidbody2D body;
    private bool canBullet = true;

    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;

    // AWAKE:
    // - get the body
    // - set up the moveAction
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // UPDATE:
    // - move around
    void Update()
    {
        PrimeBullet();

        // move the spawner around
        body.linearVelocityX = speed;
        if (body.position.x < -5)
        {
            speed = Mathf.Abs(speed);
        }
        if (body.position.x > 5)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    // PRIMEBULLET:
    // set canbullet to false
    // invoke the method for spawning a bullet
    void PrimeBullet()
    {
        if(canBullet == true)
        {
            canBullet = false;

            // prime the bullet
            Invoke(nameof(SpawnBullet), Random.Range(3, 6));
        }
    }

    // SPAWNBULLET:
    // spawn a bullet at the spawner's position
    void SpawnBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
    
    // RESETCANBULLET:
    // Set canBullet to true.
    public void ResetCanBullet()
    {
        canBullet = true;
    }
}
