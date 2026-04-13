using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BulletFalling : MonoBehaviour
{
    private Rigidbody2D body;

    private GameObject bulletSpawner;

    // AWAKE:
    // - get the body
    // - set up the moveAction
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        bulletSpawner = GameObject.Find("BulletSpawner");
    }

    // UPDATE:
    // - delete and reset bullet spawner if too low
    void Update()
    {
        if(body.transform.position.y < -15)
        {
            // reset bullet spawner
            bulletSpawner.GetComponent<BulletSpawner>().ResetCanBullet();

            // die
            Destroy(body.gameObject);
        }
    }

    // COLLECT:
    // - delete without resetting bulletSpawner
    public void Collect()
    {
        // die
        Destroy(body.gameObject);
    }
}
