using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEmotion : MonoBehaviour
{
    // nodes for movement
    [SerializeField] private GameObject centerNode;
    [SerializeField] private GameObject leftUpNode;
    [SerializeField] private GameObject rightUpNode;
    [SerializeField] private GameObject leftDownNode;
    [SerializeField] private GameObject rightDownNode;
    [SerializeField] private GameObject deathNode;
    private GameObject targetNode;

    // different attacks to instantiate
    [SerializeField] private GameObject happyProjectile;
    [SerializeField] private GameObject sadProjectile;
    [SerializeField] private GameObject madProjectile;

    [SerializeField] private Animator _animator;

    public float speed;
    public float moveRate;
    private bool moveCooldown = false;
    private int chargeUp = 0;
    private int targetCharge;

    private int randomizer;

    private bool dead = false;

    // AWAKE:
    // - Set targetNode to centerNode
    void Awake()
    {
        targetNode = centerNode;
    }

    // UPDATE:
    // - Move towards target nodes when not at them
    void Update()
    {
        if(dead == false)
        {
            // set up charge
            targetCharge = Random.Range(4, 8);

            // move if not at targetNode
            float step = speed * Time.deltaTime;

            if(transform.position != targetNode.transform.position)
                transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, step);
            else if(moveCooldown == false)
            {
                moveCooldown = true;
                if(chargeUp < targetCharge) // just move around if not charged yet
                    Invoke(nameof(MoveAround), moveRate);
                else // pick a move
                    Invoke(nameof(ChooseAttack), moveRate);
            }
        }
        else
        {
            float step = speed * Time.deltaTime;

            while(transform.position != centerNode.transform.position && targetNode != deathNode)
                transform.position = Vector3.MoveTowards(transform.position, centerNode.transform.position, step);
            print("Dead");
            _animator.SetBool("Dead", true);
            targetNode = deathNode;
            Invoke(nameof(DeathMover), 1f);
            Invoke(nameof(GoToWin), 3f);
        }
    }

    // SPEED UP:
    // - Add to speed
    // - Subtract from moveRate
    public void SpeedUp()
    {
        speed = speed + 3;
        moveRate = moveRate - 0.2f;
    }

    // MOVEAROUND:
    // - Set the targetNode to a random one.
    void MoveAround()
    {
        randomizer = Random.Range(0, 4);
        if(randomizer == 0)
            targetNode = centerNode;
        else if (randomizer == 1)
            targetNode = leftUpNode;
        else if (randomizer == 2)
            targetNode = rightUpNode;
        else if (randomizer == 3)
            targetNode = leftDownNode;
        else if (randomizer == 4)
            targetNode = rightDownNode;

        // reset moveCooldown and add to charge
        moveCooldown = false;
        chargeUp += 1;
    }
    
    // CHOOSEATTACK:
    // - Randomize which attack we're doing.
    void ChooseAttack()
    {
        randomizer = Random.Range(0, 8);
        if(randomizer >= 0 && randomizer <= 2) // 0, 1, 2; 33.3%
        {
            print("Attack one chosen.");
            StartCoroutine(HappyAttack());
        }
        else if(randomizer > 2 && randomizer <= 5) // 3, 4, 5; 33.3%
        {
            print("Attack two chosen.");
            StartCoroutine(SadAttack());
        }
        else if(randomizer >= 6) // 6, 7, 8; 33.3%
        {
            print("Attack three chosen.");
            StartCoroutine(AngryAttack());
        }
        else // What
        {
            Debug.Log("Attack randomizer out of bounds.");
            Debug.Log(randomizer);
        }
    }

    // HAPPYATTACK:
    // - Do the happy attack
    // - Reset attack when finished
    private IEnumerator HappyAttack()
    {
        // animate into happiness
        _animator.SetInteger("Emotion", 0);
        yield return new WaitForSeconds(0.5f);

        // set target node; pivot if trying to stay in the same place
        randomizer = Random.Range(0, 1);
        if(randomizer == 0)
        {
            if(targetNode == rightUpNode)
                targetNode = leftUpNode;
            else
                targetNode = rightUpNode;
        }
        else
        {
            if(targetNode == leftUpNode)
                targetNode = rightUpNode;
            else
                targetNode = leftUpNode;
        }


        // summon projectiles
        for(int i = 0; i < (speed - 3); i++)
        {
            // stop if dead
            if(dead == true)
                yield break;

            // move around if static
            if(this.transform.position == rightUpNode.transform.position)
                targetNode = leftUpNode;
            else if(this.transform.position == leftUpNode.transform.position)
                targetNode = rightUpNode;

            // summon
            Instantiate(happyProjectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
        }

        // reset
        chargeUp = 0;
        moveCooldown = false;
    }

    // SADATTACK:
    // - Do the sad attack
    // - Reset attack when finished
    private IEnumerator SadAttack()
    {
        // animate into sadness
        _animator.SetInteger("Emotion", 1);
        yield return new WaitForSeconds(0.5f);


        // set target node; pivot if trying to stay in the same place
        randomizer = Random.Range(0, 1);
        if(randomizer == 0)
        {
            if(targetNode == rightDownNode)
                targetNode = leftDownNode;
            else
                targetNode = rightDownNode;
        }
        else
        {
            if(targetNode == leftDownNode)
                targetNode = rightDownNode;
            else
                targetNode = leftDownNode;
        }


        // summon projectiles
        for(int i = 0; i < (speed + 6); i++)
        {
            // stop if dead
            if(dead == true)
                yield break;

            // move around if static
            if(this.transform.position == rightDownNode.transform.position)
                targetNode = leftDownNode;
            else if(this.transform.position == leftDownNode.transform.position)
                targetNode = rightDownNode;

            // summon
            Instantiate(sadProjectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

        // reset
        chargeUp = 0;
        moveCooldown = false;
    }

    // ANGRYATTACK:
    // - Do the angry attack
    // - Reset attack when finished
    private IEnumerator AngryAttack()
    {
        // animate into anger
        _animator.SetInteger("Emotion", 2);
        yield return new WaitForSeconds(0.5f);


        // summon projectiles
        for(int i = 0; i < 50; i++)
        {
            // stop if dead
            if(dead == true)
                yield break;

            // summon
            Instantiate(madProjectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }

        // set target node; pivot if trying to stay in the same place
        randomizer = Random.Range(0, 1);
        if(randomizer == 0)
        {
            if(targetNode == centerNode)
                targetNode = rightUpNode;
            else
                targetNode = centerNode;
        }
        else
        {
            if(targetNode == centerNode)
                targetNode = leftUpNode;
            else
                targetNode = centerNode;
        }

        yield return new WaitForSeconds(1f);

        // summon MORE projectiles
        for(int i = 0; i < 50; i++)
        {
            // stop if dead
            if(dead == true)
                yield break;

            // summon
            Instantiate(madProjectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }

        // reset
        chargeUp = 0;
        moveCooldown = false;
    }

    // BOSSHURT:
    // - Animate getting hurt
    public void BossHurt()
    {
        _animator.SetBool("Hurt", true);
        Invoke(nameof(BossUnHurt), 0.2f);
    }
    public void BossUnHurt()
    {
        _animator.SetBool("Hurt", false);
    }

    // BOSSDIE:
    // - Die
    public void BossDie()
    {
        print("deaaad");

        dead = true;
    }

    // DEATHMOVER:
    // - coughs up blood
    // - i would kill for a sensible timer function right now
    void DeathMover()
    {
        transform.position = Vector3.MoveTowards(transform.position, deathNode.transform.position, (speed * Time.deltaTime));
    }

    // GOTOTITLE:
    // - Do   that
    void GoToWin()
    {
        SceneManager.LoadScene("WinScreen");
    }
}