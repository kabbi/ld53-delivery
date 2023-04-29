using System;
using UnityEngine;
using TMPro;

public class ScoreIndicator : MonoBehaviour
{
    public enum Event
    {
        EnemyDeath,
        LiverEaten,
        LiverHealed,
    }

    [Serializable]
    public struct EventCost
    {
        public Event type;
        public float cost;
    }

    public EventCost[] costs;
    public TMP_Text label;
    private float score = 0;

    void Update()
    {
        label.text = $"Score: {score}";
    }

    public void LogEvent(Event type)
    {
        foreach (var item in costs)
        {
            if (type == item.type)
            {
                score += item.cost;
            }
        }
    }
}
