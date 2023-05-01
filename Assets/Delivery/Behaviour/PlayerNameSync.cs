using UnityEngine;
using TMPro;

public class PlayerNameSync : MonoBehaviour
{
    public TMP_InputField textInput;
    public string defaultPlayerName;

    void Start()
    {
        textInput.text = PlayerName.Instance.playerName;
    }

    void Update()
    {
        PlayerName.Instance.playerName = textInput.text;
    }
}
