using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float radius = 50;
    public float intervalMin = 2;
    public float intervalMax = 10;
    public float speedMin = 0.1f;
    public float speedMax = 3;
    public GameObject prefab;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
            Vector2 position = Random.insideUnitCircle.normalized * radius;
            GameObject enemy = Instantiate(prefab, position, Quaternion.identity);
            enemy.transform.localScale = Vector2.one * Random.Range(0.7f, 1);
            enemy.GetComponent<SimpleNavigator>().speed = Random.Range(speedMin, speedMax);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
