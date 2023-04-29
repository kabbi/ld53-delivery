using System.Collections;
using UnityEngine;

public class EatLiverBehaviour : MonoBehaviour
{
    public float interval = 2;
    public GameObject damagePrefab;
    public float sizeVariation = 1.5f;

    void Start()
    {
        StartCoroutine(Eat());
    }

    IEnumerator Eat()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject damage = Instantiate(damagePrefab, transform.position, Quaternion.identity);
            damage.transform.localScale = Vector3.one * Random.Range(0.2f, sizeVariation);
        }
    }
}
