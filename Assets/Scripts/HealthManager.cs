using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int playerHealth = 3;
    private int bossHealth = 3;

    [SerializeField] private GameObject playerHeart1;
    [SerializeField] private GameObject playerHeart2;
    [SerializeField] private GameObject playerHeart3;
    [SerializeField] private Sprite emptyPlayerHeart;

    [SerializeField] private GameObject bossHeart1;
    [SerializeField] private GameObject bossHeart2;
    [SerializeField] private GameObject bossHeart3;
    [SerializeField] private Sprite emptyBossHeart;

    public void RemovePlayerHealth()
    {
        playerHealth--;
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
        }
    }

    public void RemoveBossHealth()
    {
        bossHealth--;
        if(bossHealth == 2)
        {
            bossHeart3.gameObject.GetComponent<SpriteRenderer>().sprite = emptyBossHeart;
        }
        if(bossHealth == 1)
        {
            bossHeart2.gameObject.GetComponent<SpriteRenderer>().sprite = emptyBossHeart;
        }
        if (bossHealth == 0)
        {
            bossHeart1.gameObject.GetComponent<SpriteRenderer>().sprite = emptyBossHeart;
        }
    }
}
