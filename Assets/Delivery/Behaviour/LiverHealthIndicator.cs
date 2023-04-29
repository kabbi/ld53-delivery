using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiverHealthIndicator : MonoBehaviour
{
    public Transform damageContainer;
    public float health = 100;
    public TMP_Text label;

    void Update()
    {
        float currentHealth = health - damageContainer.childCount;
        label.text = $"Liver: {Mathf.Max(currentHealth, 0.0f)}/{health}";
    }
}
