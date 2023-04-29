using UnityEngine;

public class DestroyOnCollisionBehaviour : MonoBehaviour
{
    public string enemyTag = "Enemy";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == enemyTag)
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }
}
