using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform progressBar;
    public GameObject disabledIndicator;
    public float cooldown = 10;
    private float timeUsed;
    private bool ready;

    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry dragEntry = new EventTrigger.Entry();
        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        trigger.triggers.Add(dragEntry);

        EventTrigger.Entry beginDragEntry = new EventTrigger.Entry();
        beginDragEntry.eventID = EventTriggerType.BeginDrag;
        beginDragEntry.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
        trigger.triggers.Add(beginDragEntry);

        EventTrigger.Entry endDragEntry = new EventTrigger.Entry();
        endDragEntry.eventID = EventTriggerType.EndDrag;
        endDragEntry.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        trigger.triggers.Add(endDragEntry);

        StartCoroutine(DisabledCheck());
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

    IEnumerator DisabledCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            bool allowed = towers.Length < 6;
            disabledIndicator.SetActive(!allowed);
        }
    }

    IEnumerator Cooldown()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        eventTrigger.enabled = false;
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
        eventTrigger.enabled = true;
    }
}
