using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragSpawner : MonoBehaviour
{
    public GameObject prefab;

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
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Instantiate(gameObject, transform.parent);
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
}
