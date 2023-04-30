using System.Collections;
using UnityEngine;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    enum State
    {
        Idle,
        Walking,
        Eating,
        Blocked,
    };

    public float health = 1;
    public float waitBeforeEating = 1;
    public string liverTag = "Liver";
    private State state;
    private State stateAfterBlock;
    private SimpleNavigator navigate;
    private EatLiverBehaviour eat;
    private FreezeBehaviour freeze;
    private float currentHealth;

    void Start()
    {
        navigate = GetComponent<SimpleNavigator>();
        eat = GetComponent<EatLiverBehaviour>();
        freeze = GetComponent<FreezeBehaviour>();
        state = State.Walking;
        currentHealth = health;
    }

    void Update()
    {
        navigate.enabled = state == State.Walking;
        eat.enabled = state == State.Eating;
        freeze.enabled = state == State.Blocked;
    }

    void OnDestroy()
    {
        ScoreIndicator score = GameObject.FindAnyObjectByType<ScoreIndicator>();
        score?.LogEvent(ScoreIndicator.Event.EnemyDeath);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Block(float time)
    {
        if (state != State.Blocked)
        {
            stateAfterBlock = state;
            state = State.Blocked;
        }
        StopAllCoroutines();
        StartCoroutine(Unblock(time));
    }

    IEnumerator Unblock(float time)
    {
        yield return new WaitForSeconds(time);
        state = stateAfterBlock;
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
            StopAllCoroutines();
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
