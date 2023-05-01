using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGameButton : MonoBehaviour
{
    public string sceneName;
    public string defaultPlayerName;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
        if (defaultPlayerName != "" && PlayerName.Instance.playerName == "")
        {
            FindObjectOfType<PlayerNameSync>().enabled = false;
            PlayerName.Instance.playerName = defaultPlayerName;
        }
    }
}
