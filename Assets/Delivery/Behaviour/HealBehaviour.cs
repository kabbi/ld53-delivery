using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehaviour : MonoBehaviour
{
    public float healInterval = 3;
    public string damageTag = "Damage";
    public float radius = 10;

    void Start()
    {
        StartCoroutine(Heal());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator Heal()
    {
        while (true)
        {
            yield return new WaitForSeconds(healInterval);

            Collider2D nearest = null;
            float nearestDistance = float.PositiveInfinity;
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (var item in items)
            {
                if (item.tag != damageTag)
                {
                    continue;
                }
                float distance = Vector2.Distance(transform.position, item.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = item;
                }
            }

            if (nearest != null)
            {
                Destroy(nearest.gameObject);
            }

        }
    }
}
