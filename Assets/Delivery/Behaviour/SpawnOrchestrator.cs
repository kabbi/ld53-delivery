using System;
using System.Collections;
using UnityEngine;

public class SpawnOrchestrator : MonoBehaviour
{
    public enum ActionType
    {
        Wait,
        Enable,
        EnableSequential,
    }

    [Serializable]
    public struct Action
    {
        public ActionType type;
        public float wait;
        public float interval;
        public GameObject target;
        public GameObject[] targets;
        public Sprite waveSprite;
    }

    public SpriteRenderer currentWaveRenderer;
    public Action[] sequence;

    void Start()
    {
        StartCoroutine(Sequencer());
    }

    IEnumerator Sequencer()
    {
        while (true)
        {
            foreach (var item in sequence)
            {
                if (item.waveSprite != null)
                {
                    currentWaveRenderer.sprite = item.waveSprite;
                }
                switch (item.type)
                {
                    case ActionType.Wait:
                        yield return new WaitForSeconds(item.wait);
                        break;
                    case ActionType.Enable:
                        GameObject clone = Instantiate(item.target, transform);
                        clone.SetActive(true);
                        break;
                    case ActionType.EnableSequential:
                        foreach (var target in item.targets)
                        {
                            GameObject cloned = Instantiate(target, transform);
                            cloned.SetActive(true);
                            yield return new WaitForSeconds(item.interval);
                        }
                        break;
                }
            }
        }
    }
}
