using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnClick((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnClick(PointerEventData data)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
