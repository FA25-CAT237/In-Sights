using UnityEngine;

public class Randovelocity : MonoBehaviour
{
    private Rigidbody2D body;

    public float upperLimit;
    public float lowerLimit;
    private float velocityToGo;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        velocityToGo = Random.Range(lowerLimit, upperLimit);
    }

    // UPDATE:
    // - give self a random velocity based on limits
    void Update()
    {
        if(body != null)
            body.linearVelocityX = velocityToGo;
        else
        {
            Debug.Log("Forgot the body on a Randovelocity!");
            Destroy(this);
        }
    }
}
