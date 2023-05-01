using UnityEngine;
using UnityEditor;

public class LifebarGenerator : MonoBehaviour
{
    public SpawnOrchestrator waves;
    public GameObject iconPrefab;
    public Transform container;
    public Transform borderLeft;
    public Transform borderRight;
    public float scaleVariation = 1.3f;
    public float positionRadius = 1;

    float GetItemTime(SpawnOrchestrator.Action item)
    {
        if (item.type == SpawnOrchestrator.ActionType.Wait)
        {
            return item.wait;
        }
        if (item.type == SpawnOrchestrator.ActionType.SpawnOneByOne)
        {
            return item.interval * item.targets.Length;
        }
        return 0;
    }

    public void Construct()
    {
        float totalTime = 0;
        foreach (var item in waves.sequence)
        {
            totalTime += GetItemTime(item);
        }

        float time = 0;
        Sprite lastSprite = null;
        foreach (var item in waves.sequence)
        {
            float progress = time / totalTime;
            Vector3 position = Vector3.Lerp(borderLeft.position, borderRight.position, progress);
            if (item.waveSprite != null)
            {
                lastSprite = item.waveSprite;
            }
            if (lastSprite != null)
            {
                // int power = item.type == SpawnOrchestrator.ActionType.SpawnGroup ? item.target.transform.childCount : item.type == SpawnOrchestrator.ActionType.SpawnOneByOne ? item.targets.Length : 0;
                int power = item.type == SpawnOrchestrator.ActionType.Wait ? 0 : 1;
                for (int i = 0; i < power; i++)
                {
                    Vector2 offset = Random.insideUnitCircle * positionRadius;
                    Vector3 iconPosition = position + new Vector3(offset.x, offset.y, 0);
                    GameObject icon = Instantiate(iconPrefab, iconPosition, Quaternion.identity, container);
                    icon.transform.localScale = icon.transform.localScale * Random.Range(1, scaleVariation);
                    icon.GetComponent<SpriteRenderer>().sprite = item.waveSprite;
                }
            }
            time += GetItemTime(item);
        }
    }

    public void Clear()
    {
        int childCount = container.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(container.GetChild(0).gameObject);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LifebarGenerator))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LifebarGenerator lifebar = (LifebarGenerator)target;

        DrawDefaultInspector();
        if (GUILayout.Button("Construct life"))
        {
            lifebar.Construct();
        }
        if (GUILayout.Button("Destroy life"))
        {
            lifebar.Clear();
        }
    }
}
#endif
