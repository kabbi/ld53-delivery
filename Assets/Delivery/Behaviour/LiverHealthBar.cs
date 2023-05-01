using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiverHealthBar : MonoBehaviour
{
    public Transform damageContainer;
    public Transform mask;
    public Transform indicator;
    public Vector2 indicatorRange;
    public Vector2 maskRange;
    public float health = 100;
    public TMP_Text label;
    private float maskMaxScale;

    void Start()
    {
        maskMaxScale = mask.localScale.x;
    }

    void Update()
    {
        float currentHealth = health - damageContainer.childCount;
        label.text = $"liver {Mathf.Max(currentHealth / health * 100, 0.0f)}%";

        float progress = currentHealth / health;
        Vector3 indicatorPosition = indicator.localPosition;
        indicatorPosition.x = Mathf.Lerp(indicatorRange.x, indicatorRange.y, progress);
        indicator.localPosition = indicatorPosition;

        Vector3 maskPosition = mask.localPosition;
        Vector3 maskScale = mask.localScale;
        maskScale.x = Mathf.Lerp(0, maskMaxScale, progress);
        maskPosition.x = Mathf.Lerp(maskRange.x, maskRange.y, progress);
        mask.localPosition = maskPosition;
        mask.localScale = maskScale;
    }
}
