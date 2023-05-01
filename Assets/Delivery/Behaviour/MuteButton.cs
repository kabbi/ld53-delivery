using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MuteButton : MonoBehaviour
{
    public Sprite muteSprite;
    public Sprite unmuteSprite;
    public SpriteRenderer sprite;
    private bool muted;

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
        muted = !muted;
        AudioListener.volume = muted ? 0 : 1;
        sprite.sprite = muted ? unmuteSprite : muteSprite;
    }
}
