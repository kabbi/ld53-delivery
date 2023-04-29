using System.Collections;
using UnityEngine;

public class DestroyBehaviour : MonoBehaviour
{
    public float time = 5;

    void Start()
    {
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
