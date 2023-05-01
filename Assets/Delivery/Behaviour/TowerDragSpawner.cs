using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform progressBar;
    public GameObject disabledIndicator;
    public SpriteRenderer sprite;
    public Sprite disabledSprite;
    public float cooldown = 10;
    public float maxTowers = 6;
    private EventTrigger eventTrigger;
    private Sprite enabledSprite;
    private float timeUsed;
    private bool cooldownOk = true;
    private bool limitOk = true;

    void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(dragEntry);

        EventTrigger.Entry beginDragEntry = new EventTrigger.Entry();
        beginDragEntry.eventID = EventTriggerType.BeginDrag;
        beginDragEntry.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(beginDragEntry);

        EventTrigger.Entry endDragEntry = new EventTrigger.Entry();
        endDragEntry.eventID = EventTriggerType.EndDrag;
        endDragEntry.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(endDragEntry);

        StartCoroutine(LimitCheck());
    }

    void Update()
    {
        eventTrigger.enabled = cooldownOk && limitOk;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        GameObject button = Instantiate(gameObject, transform.parent);
        button.GetComponent<TowerDragSpawner>().StartCooldown();
    }

    public void OnDrag(PointerEventData data)
    {
        Ray ray = Camera.main.ScreenPointToRay(data.position);
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        rayPoint.z = 0;
        transform.position = rayPoint;
    }

    public void OnEndDrag(PointerEventData data)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator LimitCheck()
    {
        enabledSprite = sprite.sprite;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            limitOk = towers.Length < maxTowers;
            sprite.sprite = limitOk ? enabledSprite : disabledSprite;
        }
    }

    IEnumerator Cooldown()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        cooldownOk = false;
        timeUsed = Time.time;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Time.time - timeUsed >= cooldown)
            {
                break;
            }
            float progress = (Time.time - timeUsed) / cooldown;
            float halfProgress = progress / 2;
            progressBar.transform.localPosition = new Vector3(0, -0.5f + halfProgress, 0);
            progressBar.transform.localScale = new Vector3(2, progress * 2, 2);
        }
        cooldownOk = true;
    }
}
