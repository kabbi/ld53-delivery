using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTowerBehaviour : MonoBehaviour
{
    public float shootInterval = 3;
    public float radius = 10;
    public GameObject bulletPrefab;
    public float bulletSpeed = 3;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            Collider2D nearest = null;
            float nearestDistance = float.PositiveInfinity;
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, radius);
            Debug.Log(items);
            foreach (var item in items)
            {
                if (item.tag != "Enemy")
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
                GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.localRotation);
                Vector2 force = (nearest.transform.position - transform.position).normalized * bulletSpeed;
                bullet.GetComponent<Rigidbody2D>().AddForce(force);
            }

        }
    }
}
