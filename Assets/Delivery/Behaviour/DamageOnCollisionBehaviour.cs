using UnityEngine;

public class DamageOnCollisionBehaviour : MonoBehaviour
{
    public float damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        SimpleEnemyBehaviour enemy = collision.collider.GetComponent<SimpleEnemyBehaviour>();
        if (enemy != null)
        {
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }
}
