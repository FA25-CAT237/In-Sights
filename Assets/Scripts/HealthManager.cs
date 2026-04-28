using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int playerHealth = 3;
    private int bossHealth = 3;

    [SerializeField] private GameObject playerHeart1;
    [SerializeField] private GameObject playerHeart2;
    [SerializeField] private GameObject playerHeart3;
    [SerializeField] private Sprite emptyPlayerHeart;
    private bool playerHurtCooldown;

    [SerializeField] private GameObject bossHeart1;
    [SerializeField] private GameObject bossHeart2;
    [SerializeField] private GameObject bossHeart3;
    [SerializeField] private Sprite emptyBossHeart;

    [SerializeField] private GameObject theBoss;
    [SerializeField] private GameObject thePlayer;

    public void RemovePlayerHealth()
    {
        if(playerHurtCooldown == false)
        {
            playerHealth--;
            playerHurtCooldown = true;
            Invoke(nameof(ResetPlayerHurtCooldown), 2f);
        }
        if (playerHealth == 2)
        {
            playerHeart3.gameObject.GetComponent<SpriteRenderer>().sprite = emptyPlayerHeart;
        }
        if (playerHealth == 1)
        {
            playerHeart2.gameObject.GetComponent<SpriteRenderer>().sprite = emptyPlayerHeart;
        }
        if (playerHealth == 0)
        {
            playerHeart1.gameObject.GetComponent<SpriteRenderer>().sprite = emptyPlayerHeart;

            // player is game over
            thePlayer.GetComponent<PlayerCharacter>().StartPlayerDie();
        }
    }

    public void RemoveBossHealth()
    {
        bossHealth--;
        if(bossHealth == 2)
        {
            bossHeart3.gameObject.GetComponent<SpriteRenderer>().sprite = emptyBossHeart;
            theBoss.GetComponent<BossEmotion>().BossHurt();
        }
        if(bossHealth == 1)
        {
            bossHeart2.gameObject.GetComponent<SpriteRenderer>().sprite = emptyBossHeart;
            theBoss.GetComponent<BossEmotion>().BossHurt();
        }
        if (bossHealth == 0)
        {
            bossHeart1.gameObject.GetComponent<SpriteRenderer>().sprite = emptyBossHeart;
            theBoss.GetComponent<BossEmotion>().BossHurt();

            // boss is dead
            theBoss.GetComponent<BossEmotion>().BossDie();
        }
    }

    void ResetPlayerHurtCooldown()
    {
        playerHurtCooldown = false;
    }
}
