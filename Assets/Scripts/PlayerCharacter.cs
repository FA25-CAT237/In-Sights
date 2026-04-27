using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// THIS IS THE SCRIPT FOR "In Sights" AAA
public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private float speed;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashLength;
    [SerializeField] private float dashCDTimer;
    private bool isDashing = false;
    private bool dashCooldown = false;

    private bool loaded = false;
    [SerializeField] private GameObject crosshair;

    // actions
    InputAction moveAction;
    InputAction dashAction;
    InputAction shootAction;

    // animations
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject healthManager;


    // AWAKE:
    // - get the body
    // - set up the actions
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        moveAction = InputSystem.actions.FindAction("Move");
        dashAction = InputSystem.actions.FindAction("Sprint");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    // UPDATE:
    // - handle movement
    // - handle animating directions
    // - handle dashing
    // - 
    void Update()
    {
        // get moveValue and use it to move
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
            
        if ((Mathf.Abs(moveValue.x) > 0 || Mathf.Abs(moveValue.y) > 0) && isDashing == false)
        {
            body.linearVelocityX = moveValue.x * speed;
            body.linearVelocityY = moveValue.y * speed;
        }
        /*else if((Mathf.Abs(moveValue.x) > 0 || Mathf.Abs(moveValue.y) > 0) && isDashing == true)
        {
            body.linearVelocityX = moveValue.x * dashPower;
            body.linearVelocityY = moveValue.y * dashPower;
        }*/
        else if(isDashing == false)
        {
            body.linearVelocity = Vector2.zero;
        }

        // get direction
        if (moveValue.x < 0)
        {
            _animator.SetBool("isLeft", true);
            _animator.SetBool("isRight", false);
        }
        else if (moveValue.x > 0)
        {
            _animator.SetBool("isLeft", false);
            _animator.SetBool("isRight", true);
        }
        else
        {
            _animator.SetBool("isLeft", false);
            _animator.SetBool("isRight", false);
        }

        // handle dashing
        if (dashAction.IsPressed() && dashCooldown == false && moveValue != Vector2.zero)
        {
            StartCoroutine(Dash());
            dashCooldown = true;
        }

        // handle shooting
        if (shootAction.IsPressed() && loaded == true)
        {
            // become unloaded
            loaded = false;
            _animator.SetBool("isLoaded", false);
        }
    }

    // DASH:
    // - Dash, animate, and reset the cooldown after a delay.
    private IEnumerator Dash()
    {
        isDashing = true;
        _animator.SetBool("isDashing", true);
        body.linearVelocity = new Vector2(body.linearVelocityX * dashPower, body.linearVelocityY * dashPower);
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
        _animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCDTimer);
        dashCooldown = false;
    }

    // ONTRIGGERENTER2D:
    // - Check and collect if a Bullet
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            // collect the bullet
            other.gameObject.GetComponent<BulletFalling>().Collect();

            // set loaded and animations to true
            loaded = true;
            _animator.SetBool("isLoaded", true);

            // instantiate a crosshair
            Invoke(nameof(SpawnCrosshair), 0.5f);
        }
        else if(other.gameObject.tag == "BossAttack" || other.gameObject.tag == "Boss")
        {
            // ow
            healthManager.GetComponent<HealthManager>().RemovePlayerHealth();
        }
    }

    // SPAWNCROSSHAIR:
    // - Instantiate a crosshair
    // - This is a function so that we can Invoke it.
    private void SpawnCrosshair()
    {
        Instantiate(crosshair, transform.position, Quaternion.identity);
    }

    // STARTPLAYERDIE:
    // - i dont know girl
    // - this lets other scripts call playerdie as a coroutine
    public void StartPlayerDie()
    {
        StartCoroutine(PlayerDie());
    }

    // PLAYERDIES:
    // - Play an animation
    // - Load the death screen
    private IEnumerator PlayerDie()
    {
        // animate
        print("Huh?");
        yield return new WaitForSeconds(2);
        print("What?");
        SceneManager.LoadScene("TitleScreen");
    }
}
