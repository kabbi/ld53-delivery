using UnityEngine;
using TMPro;

public class LifeProgressBar : MonoBehaviour
{
    public SpawnOrchestrator spawner;
    public Transform indicator;
    public Transform indicatorMin;
    public Transform indicatorMax;
    public TMP_Text label;
    public float minAge = 14;
    public float maxAge = 80;
    private float lifeExpectancy;
    private PlayerName persistedPlayerName;

    void Start()
    {
        lifeExpectancy = 0;
        persistedPlayerName = FindObjectOfType<PlayerName>();
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
        label.text = $"{persistedPlayerName.playerName}: {ageYears.ToString("F2")} y.o.";

        indicator.position = Vector3.Lerp(indicatorMin.position, indicatorMax.position, progress); ;
    }
}
