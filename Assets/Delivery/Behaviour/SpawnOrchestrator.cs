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
    }

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
                switch (item.type)
                {
                    case ActionType.Wait:
                        yield return new WaitForSeconds(item.wait);
                        break;
                    case ActionType.Enable:
                        item.target.SetActive(true);
                        break;
                    case ActionType.EnableSequential:
                        foreach (var target in item.targets)
                        {
                            target.SetActive(true);
                            yield return new WaitForSeconds(item.interval);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
