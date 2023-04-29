using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public float blockInterval = 1;
    public float activeInterval = 5;
    public float inactiveInterval = 10;
    public string enemyTag = "Enemy";
    public GameObject halo;
    public float radius = 10;

    void Start()
    {
        StartCoroutine(Blink());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            halo.SetActive(true);
            Coroutine healing = StartCoroutine(Heal());
            yield return new WaitForSeconds(activeInterval);
            halo.SetActive(false);
            StopCoroutine(healing);
            yield return new WaitForSeconds(inactiveInterval);
        }
    }

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(blockInterval);

            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (var item in items)
            {
                if (item.tag != enemyTag)
                {
                    continue;
                }

                SimpleEnemyBehaviour enemy = item.GetComponent<SimpleEnemyBehaviour>();
                enemy.Block(blockInterval + 0.1f);
            }
        }
    }
}
