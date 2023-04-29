using UnityEngine;

public class FreezeBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    void OnEnable()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void OnDisable()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
