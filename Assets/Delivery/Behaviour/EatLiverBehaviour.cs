using System.Collections;
using UnityEngine;

public class EatLiverBehaviour : MonoBehaviour
{
    public float interval = 2;
    public GameObject damagePrefab;
    public float sizeVariation = 1.5f;
    public string targetTag;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        StartCoroutine(Eat());
    }

    IEnumerator Eat()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject damage = Instantiate(damagePrefab, transform.position, Quaternion.identity, target);
            damage.transform.localScale = Vector3.one * Random.Range(0.2f, sizeVariation) / target.localScale.x;

            ScoreIndicator score = GameObject.FindAnyObjectByType<ScoreIndicator>();
            score?.LogEvent(ScoreIndicator.Event.LiverEaten);
        }
    }
}
