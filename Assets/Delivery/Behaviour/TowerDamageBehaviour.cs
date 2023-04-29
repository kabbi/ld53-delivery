using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDamageBehaviour : MonoBehaviour
{
    public GameObject[] damageIndicators;
    public string enemyTag = "Enemy";
    private int hits = 0;
    private int health;

    void Start()
    {
        health = damageIndicators.Length;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == enemyTag)
        {
            hits += 1;
            UpdateIndicators();
            if (hits >= health)
            {
                Destroy(gameObject);
            }
        }
    }

    void UpdateIndicators()
    {
        for (int i = 0; i < damageIndicators.Length; i++)
        {
            damageIndicators[i].SetActive(hits > i);
        }
    }
}
