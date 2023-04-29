using System.Collections;
using UnityEngine;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    enum State
    {
        Idle,
        Walking,
        Eating,
    };

    private State state;
    private SimpleNavigator navigator;
    public float waitBeforeEating = 1;

    void Start()
    {
        navigator = GetComponent<SimpleNavigator>();
        state = State.Walking;
    }

    void Update()
    {
        navigator.enabled = state == State.Walking;
    }

    IEnumerator TransitionToEating()
    {
        yield return new WaitForSeconds(waitBeforeEating);
        state = State.Eating;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Liver")
        {
            StartCoroutine(TransitionToEating());
        }
    }
}
