using UnityEngine;
using TMPro;

public class LifeProgressBar : MonoBehaviour
{
    public SpawnOrchestrator spawner;
    public Transform mask;
    public Transform indicator;
    public Vector2 indicatorRange;
    public Vector2 maskRange;
    public TMP_Text label;
    public string playerName;
    public float minAge = 14;
    public float maxAge = 80;
    private float lifeExpectancy;
    private float maskMaxScale;

    void Start()
    {
        maskMaxScale = mask.localScale.x;

        lifeExpectancy = 0;
        foreach (var item in spawner.sequence)
        {
            if (item.type == SpawnOrchestrator.ActionType.Wait)
            {
                lifeExpectancy += item.wait;
            }
            if (item.type == SpawnOrchestrator.ActionType.SpawnOneByOne)
            {
                lifeExpectancy += item.interval * item.targets.Length;
            }
        }
    }

    public float GetAgeYears()
    {
        float ageYears = minAge + (maxAge - minAge) * GetProgress();
        return ageYears;
    }

    public float GetProgress()
    {
        float progress = Mathf.Clamp(Time.timeSinceLevelLoad / lifeExpectancy, 0, 1);
        return progress;
    }

    void Update()
    {
        float progress = GetProgress();
        float ageYears = GetAgeYears();
        label.text = $"Age: {ageYears.ToString("F2")} years";

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
