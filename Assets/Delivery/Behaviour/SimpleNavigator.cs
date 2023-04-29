using UnityEngine;

public class SimpleNavigator : MonoBehaviour
{
    public Vector2 target;
    public float speed;
    public Rigidbody2D rigidBody;

    void FixedUpdate()
    {
        Vector2 direction = (target - rigidBody.position).normalized;
        rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
    }
}
