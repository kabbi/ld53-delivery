using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string sceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
