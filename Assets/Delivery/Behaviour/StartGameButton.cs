using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGameButton : MonoBehaviour
{
    public string sceneName;
    public TMP_Text nameInput;
    private string playerName;

    public void StartGame()
    {
        playerName = nameInput.GetParsedText();
        AsyncOperation job = SceneManager.LoadSceneAsync(sceneName);
        job.completed += SetPlayerName;
    }

    void SetPlayerName(AsyncOperation op)
    {
        LifeProgressBar life = GameObject.FindObjectOfType<LifeProgressBar>();
        life.playerName = playerName;
    }
}
