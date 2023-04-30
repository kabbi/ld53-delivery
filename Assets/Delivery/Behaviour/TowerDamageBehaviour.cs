using UnityEngine;

public class TowerDamageBehaviour : MonoBehaviour
{
    public Sprite[] damageSprites;
    public SpriteRenderer spriteRenderer;
    public string enemyTag = "Enemy";
    private int hits = 0;
    private int health;

    void Start()
    {
        health = damageSprites.Length + 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == enemyTag)
        {
            hits += 1;
            UpdateIndicators();
            if (hits >= health)
            {
                Destroy(gameObject);
            }
        }
    }

    void UpdateIndicators()
    {
        if (hits > 0 && hits < health)
        {
            spriteRenderer.sprite = damageSprites[hits - 1];
        }
    }
}
