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
    private SimpleNavigator navigate;
    private EatLiverBehaviour eat;
    public float waitBeforeEating = 1;
    public string liverTag = "Liver";

    void Start()
    {
        navigate = GetComponent<SimpleNavigator>();
        eat = GetComponent<EatLiverBehaviour>();
        state = State.Walking;
    }

    void Update()
    {
        navigate.enabled = state == State.Walking;
        eat.enabled = state == State.Eating;
    }

    IEnumerator TransitionToEating()
    {
        yield return new WaitForSeconds(waitBeforeEating);
        state = State.Eating;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == liverTag)
        {
            StartCoroutine(TransitionToEating());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == liverTag)
        {
            state = State.Walking;
        }
    }
}
