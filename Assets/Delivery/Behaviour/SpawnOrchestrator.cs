using System;
using System.Collections;
using UnityEngine;

public class SpawnOrchestrator : MonoBehaviour
{
    public enum ActionType
    {
        Wait,
        SpawnGroup,
        SpawnOneByOne,
    }

    [Serializable]
    public struct Action
    {
        public string name;
        public ActionType type;
        public float wait;
        public float interval;
        public GameObject target;
        public GameObject[] targets;
        public Sprite waveSprite;
        public AudioClip startSound;
        public float soundVolume;
    }

    public SpriteRenderer currentWaveRenderer;
    public AudioSource soundPlayer;
    public int startFromIndex = 0;
    public Action[] sequence;

    void Start()
    {
        StartCoroutine(Sequencer());
    }

    IEnumerator Sequencer()
    {
        while (true)
        {
            for (int i = startFromIndex; i < sequence.Length; i++)
            {
                Action item = sequence[i];
                if (item.waveSprite != null)
                {
                    currentWaveRenderer.sprite = item.waveSprite;
                }
                if (item.startSound != null)
                {
                    soundPlayer.PlayOneShot(item.startSound, item.soundVolume);
                }
                switch (item.type)
                {
                    case ActionType.Wait:
                        yield return new WaitForSeconds(item.wait);
                        break;
                    case ActionType.SpawnGroup:
                        GameObject clone = Instantiate(item.target, transform);
                        clone.SetActive(true);
                        break;
                    case ActionType.SpawnOneByOne:
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
